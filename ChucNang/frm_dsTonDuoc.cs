using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLBV.ChucNang
{
    public partial class frm_dsTonDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_dsTonDuoc()
        {
            InitializeComponent();
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            loaddsTonDUoc();
        }
        void loaddsTonDUoc()
        {
            string timkiem=searchControl1.Text.ToUpper();
            var q = (from dv in _ldv
                     join td in _ltonduoc on dv.MaDV equals td.MaDV
                     join kp in _lkp on td.MaKho equals kp.MaKP
                     where (timkiem == "" ? true : dv.TenDV.ToUpper().Contains(timkiem))
                     select new { dv.TenDV, td.MaDV, td.MaKho, td.SoLuongTon, td.DonGia, kp.TenKP }).ToList();
            grcTonDuoc.DataSource = q;
        }
        QLBV_Database.QLBVEntities db;
        List<TonDuoc> _ltonduoc = new List<TonDuoc>();
        List<DichVu> _ldv = new List<DichVu>();
        List<KPhong> _lkp = new List<KPhong>();
        private void frm_dsTonDuoc_Load(object sender, EventArgs e)
        {
             db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            _ltonduoc = db.TonDuocs.ToList();
            _lkp = db.KPhongs.ToList();
            _ldv = db.DichVus.Where(p => p.PLoai == 1).ToList();
            if (DungChung.Bien.PLoaiKP == DungChung.Bien.st_PhanLoaiKP.Admin)
                btnupdate.Enabled = true;
            else
                btnupdate.Enabled = false;
            loaddsTonDUoc();
        }

        private void grcTonDuoc_Click(object sender, EventArgs e)
        {

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var qnd = (from nhap in db.NhapDs
                       join nhapct in db.NhapDcts on nhap.IDNhap equals nhapct.IDNhap
                       group new { nhapct, nhap } by new { nhapct.MaDV, nhapct.DonGia, nhap.MaKP } into kq
                       select new {kq.Key.MaDV, kq.Key.MaKP, kq.Key.DonGia, SoLuong = (kq.Sum(p => p.nhapct.SoLuongN) - kq.Sum(p => p.nhapct.SoLuongX)) }).ToList();
            var q = qnd.Where(p => p.SoLuong > 0);
            foreach (var item in q)
            {
                var kttonduoc = db.TonDuocs.Where(p => p.MaDV == item.MaDV && p.MaKho == item.MaKP && p.DonGia == item.DonGia).SingleOrDefault();
                if (kttonduoc == null)
                {
                    TonDuoc moi = new TonDuoc();
                    moi.DonGia = item.DonGia;
                    moi.MaDV = item.MaDV??0;
                    moi.MaKho = item.MaKP??0;
                    moi.SoLuongTon = Math.Round(item.SoLuong,3);
                    db.TonDuocs.Add(moi);
                    db.SaveChanges();
                }
                else
                {
                    kttonduoc.SoLuongTon = Math.Round(item.SoLuong, 3);
                    db.SaveChanges();
                }
            }
            MessageBox.Show("cập nhật thành công!");
            frm_dsTonDuoc_Load(sender, e);
        }
    }
}