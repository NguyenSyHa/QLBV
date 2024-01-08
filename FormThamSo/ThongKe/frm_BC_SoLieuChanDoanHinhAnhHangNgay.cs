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
    public partial class frm_BC_SoLieuChanDoanHinhAnhHangNgay : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_SoLieuChanDoanHinhAnhHangNgay()
        {
            InitializeComponent();
        }

        private void frm_BC_SoLieuChanDoanHinhAnhHangNgay_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = DateTime.Now;
            dateDenNgay.DateTime = DateTime.Now;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime _tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);
            DateTime _denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);
            if (_denngay >= _tungay)
            {
                List<SoLieuChanDoan> _listContent = new List<SoLieuChanDoan>();
                SoLieuChanDoan moi = new SoLieuChanDoan();
                var query = (from cd in data.DThuoccts.Where(p => p.NgayNhap >= _tungay && p.NgayNhap <= _denngay)
                             join dv in data.DichVus.Where(p => p.PLoai == 2) on cd.MaDV equals dv.MaDV
                             join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                             //join cls in data.DThuocs on cd.IDDon equals cls.IDDon
                             select new
                             {
                                 //cd.Status,
                                 tn.TenRG,
                                 tn.IDNhom,
                                 dv.Loai,
                                 cd.IDDonct,
                                 dv.TenDV,
                                 dv.DonGia,
                                 cd.NgayNhap,
                                 //cls.MaBNhan
                             }).ToList();// nhóm là chẩn đoán hình ảnh
                           //.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm) || p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang) ||
                           //       p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi) || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).ToList();

                for (DateTime i = _tungay; i <= _denngay; i = i.AddDays(1))
                {
                    moi = new SoLieuChanDoan();                   
                    var q1 = query.Where(p=>p.NgayNhap.Value.Date == i.Date);
                    moi.Ngay = i.ToShortDateString();
                    moi.SA_Tong = q1.Where(p => p.TenRG.Contains(DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm) ).Count();
                    moi.SA_Thuong = q1.Where(p => (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm)).Count();
                    moi.SA_TimMach = q1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Count();
                    moi.XQ_Tong = q1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Count();
                    moi.XQ_Thuong = q1.Where(p => (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && !p.TenDV.ToLower().Contains("(kts)") && !p.TenDV.ToLower().Contains("tại giường"))).Count();
                    moi.XQ_KTS = q1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.TenDV.ToLower().Contains("(kts)")).Count();
                    moi.XQ_TaiGiuong = q1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang && p.TenDV.ToLower().Contains("tại giường")).Count();
                    moi.NoiSoi_Tong = q1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi).Count();
                    moi.NoiSoi_DaDay = q1.Where(p => (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.TenDV.ToLower().Contains("dạ dày"))).Count();
                    moi.NoiSoi_DaiTrang = q1.Where(p => (p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.NoiSoi && p.TenDV.ToLower().Contains("đại tràng"))).Count();
                    moi.DienTim = q1.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.DienTim).Count();
                    _listContent.Add(moi);
                }

                BaoCao.Rep_BC_SoLieuChanDoanHinhAnhHangNgay rep = new BaoCao.Rep_BC_SoLieuChanDoanHinhAnhHangNgay();
                frmIn frm = new frmIn();
                rep.lbl_tungaydenngay.Text = "(Báo cáo từ ngày " + _tungay.ToString("dd/MM/yyyy") + " đến ngày " + _denngay.ToString("dd/MM/yyyy") + ")";
                rep.DataSource = _listContent.Where(p => p.SA_Tong != 0 ||  p.XQ_Tong != 0 || p.NoiSoi_Tong != 0 || p.DienTim != 0);
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đến ngày không thể nhỏ hơn Từ ngày", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class SoLieuChanDoan
    {
        public string Ngay { get; set; }
        public int? SA_Tong { get; set; }
        public int? SA_Thuong { get; set; }
        public int? SA_TimMach { get; set; }
        public int? XQ_Tong { get; set; }
        public int? XQ_Thuong { get; set; }
        public int? XQ_KTS { get; set; }
        public int? XQ_TaiGiuong { get; set; }
        public int? NoiSoi_Tong { get; set; }
        public int? NoiSoi_DaDay { get; set; }
        public int? NoiSoi_DaiTrang { get; set; }
        public int? DienTim { get; set; }
    }
}