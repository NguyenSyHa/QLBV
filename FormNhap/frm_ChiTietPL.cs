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
    public partial class frm_ChiTietPL : DevExpress.XtraEditors.XtraForm
    {
        public frm_ChiTietPL(int sopl)
        {
            InitializeComponent();
            _sopl = sopl;
        }
        int _sopl = 0;
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_ChiTietPL_Load(object sender, EventArgs e)
        {
            LoadChiTiet();
        }
        void LoadChiTiet()
        {
            int i = 1;
            grvChiTietDt.ViewCaption = "Chi tiết phiếu lĩnh: " + _sopl;
            var _lthuoc = (from dt in _data.DThuoccts.Where(p => p.SoPL == _sopl)
                           join dv in _data.DichVus on dt.MaDV equals dv.MaDV
                           group new { dt, dv } by new { dt.MaDV, dt.DonVi, dt.DonGia, dv.TenDV } into kq
                           select new
                           {
                               kq.Key.MaDV,
                               kq.Key.TenDV,
                               kq.Key.DonVi,
                               kq.Key.DonGia,
                               SoLuong = kq.Sum(p => p.dt.SoLuong),
                               ThanhTien = kq.Sum(p => p.dt.ThanhTien)
                           }).OrderBy(p => p.TenDV).ToList();
            grcChiTietDt.DataSource = _lthuoc;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grcChiTietDt_Click(object sender, EventArgs e)
        {

        }
    }
}