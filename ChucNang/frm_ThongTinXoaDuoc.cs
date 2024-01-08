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
    public partial class frm_ThongTinXoaDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_ThongTinXoaDuoc()
        {
            InitializeComponent();
        }

        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_ThongTinXoaDuoc_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
        }
        private void timkiem() 
        {
            DateTime ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime ngayden = DungChung.Ham.NgayTu(lupDenNgay.DateTime);
            //var q = //(from dtct in _data.NhapDct_b.Where(p => p.ThoiGian >= ngaytu && p.ThoiGian <= ngayden)
            //         (from dtct in _data.NhapDct.Where(p => p.ThoiGian >= ngaytu && p.ThoiGian <= ngayden)
            //         join dv in _data.DichVus on dtct.MaDV equals dv.MaDV
            //         join cc in _data.NhaCCs on dtct.MaCC equals cc.MaCC
            //         select new
            //         {
            //             dtct.IDNhap,
            //             dtct.IDNhapct,
            //             dv.TenDV,
            //             dtct.DonGia,
            //             dtct.DonGiaCT,
            //             dtct.DonGiaDY,
            //             dtct.DonVi,
            //             dtct.SoLuongN,
            //             dtct.SoLuongX,
            //             dtct.SoLuongKK,
            //             dtct.SoLuongDY,
            //             dtct.ThanhTienN,
            //             dtct.ThanhTienX,
            //             dtct.ThanhTienKK,
            //             dtct.ThanhTienSD,
            //             cc.TenCC,
            //             cc.NguoiCC,
            //             dtct.ThucHien,
            //             dtct.ThoiGian
            //         }).ToList();
            var q =_data.NhapDcts.ToList();
            grcXoaDuoc.DataSource = q;
        }
        private void lupTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }

        private void lupDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            timkiem();
        }
    }
}