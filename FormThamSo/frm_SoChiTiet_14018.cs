using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;
using QLBV.DungChung;
using OpenXmlPackaging;
using System.IO;

namespace QLBV.FormThamSo
{

    public partial class frm_SoChiTiet_14018 : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoChiTiet_14018()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _db = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            if (kt())
            {
                DateTime ngaytu = System.DateTime.Now.Date;
                DateTime ngayden = System.DateTime.Now.Date;
                ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                int maDV = 0;
                int maKP = 0;
                int.TryParse(cboThuoc.EditValue.ToString(), out maDV);
                int.TryParse(cboKho.EditValue.ToString(), out maKP);
                var dichvu = _db.DichVus.FirstOrDefault(o => o.MaDV == maDV);
                var khoaPhong = _db.KPhongs.FirstOrDefault(o => o.MaKP == maKP);

                var nhapD = (from nd in _db.NhapDs.Where(o => o.NgayNhap >= ngaytu && o.NgayNhap <= ngayden && (o.PLoai == 1 || o.PLoai == 2) && o.MaKP == maKP)
                             join ndct in _db.NhapDcts on nd.IDNhap equals ndct.IDNhap
                             join dv in _db.DichVus.Where(o => o.MaDV == maDV) on ndct.MaDV equals dv.MaDV
                             select new { nd.NgayNhap, nd.SoCT, ndct.DonGia, ndct.SoLuongN, ndct.SoLuongX, ndct.ThanhTienN, ndct.ThanhTienX, ndct.GhiChu, nd.PLoai, dv.TenDV, dv.MaDV }).OrderBy(o => o.NgayNhap).ToList();

                List<SoChiTiet> soChiTiet = new List<SoChiTiet>();
                foreach (var item in nhapD)
                {
                    SoChiTiet ct = new SoChiTiet();
                    ct.MaDV = item.MaDV;
                    ct.DonGia = item.DonGia;
                    ct.GhiChu = item.GhiChu;
                    ct.NgayNhap = item.NgayNhap;
                    ct.TenDV = item.TenDV;
                    if (item.PLoai == 1)
                    {
                        ct.TonCK_SL = item.SoLuongN;
                        ct.TonCK_TT = item.ThanhTienN;
                        ct.SoCT = "N_" + item.SoCT;
                        ct.SoLuongN = item.SoLuongN;
                        ct.ThanhTienN = item.ThanhTienN;
                    }
                    else
                    {
                        ct.TonCK_SL = null;
                        ct.TonCK_TT = null;
                        ct.SoCT = "X_" + item.SoCT;
                        ct.SoLuongX = item.SoLuongX;
                        ct.ThanhTienX = item.ThanhTienX;
                    }
                    soChiTiet.Add(ct);
                }

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("TenSo", "SỔ CHI TIẾT NGUYÊN LIỆU, VẬT LIỆU, CÔNG CỤ, DỤNG CỤ, SẢN PHẨM, HÀNG HÓA");
                dic.Add("TuNgay", ngaytu.Date.ToString("dd/MM/yyyy"));
                dic.Add("DenNgay", ngayden.Date.ToString("dd/MM/yyyy"));
                dic.Add("TuNgayDenNgay", string.Format("Từ ngày {0} đến ngày {1}", ngaytu.Date.ToString("dd/MM/yyyy"), ngayden.Date.ToString("dd/MM/yyyy")));
                dic.Add("NgayThang", string.Format("Tháng {0} năm {1}", ngaytu.Month, ngaytu.Year));
                dic.Add("TenKho", string.Format("Kho: {0}", khoaPhong.TenKP));
                dic.Add("TenThuoc", string.Format("Tên nguyên liệu, vật liệu, công cụ, dụng cụ, sản phẩm, hàng hóa: {0}", dichvu.TenDV));
                dic.Add("DonViTinh", string.Format("Đơn vị tính: {0} - Quy cách, phẩm chất:.....", dichvu.DonVi));

                DungChung.Ham.Print(DungChung.PrintConfig.Rep_SoChiTiet_14018_ID550, soChiTiet, dic, false);
            }
        }

        public class SoChiTiet
        {
            public string SoCT { get; set; }
            public DateTime? NgayNhap { get; set; }
            public int MaDV { get; set; }
            public string TenDV { get; set; }
            public double DonGia { get; set; }
            public double? SoLuongN { get; set; }
            public double? SoLuongX { get; set; }
            public double? ThanhTienN { get; set; }
            public double? ThanhTienX { get; set; }
            public string GhiChu { get; set; }
            public double? TonCK_SL { get; set; }
            public double? TonCK_TT { get; set; }
        }

        private void frm_80ct_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kPhong = _dataContext.KPhongs.Where(o => o.PLoai == "Khoa dược").ToList();
            cboKho.Properties.DataSource = kPhong;
        }

        private bool kt()
        {
            if (string.IsNullOrEmpty(lupNgaytu.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupNgaytu.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lupngayden.Text))
            {
                MessageBox.Show("Bạn hãy chọn ngày tháng");
                lupngayden.Focus();
                return false;
            }
            else if ((lupngayden.DateTime - lupNgaytu.DateTime).Days < 0)
            {
                MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ");
                lupngayden.Focus();
                return false;
            }

            if (cboThuoc.EditValue == null)
            {
                MessageBox.Show("Bạn hãy chọn thuốc");
                cboThuoc.Focus();
                return false;
            }
            return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboKho_EditValueChanged(object sender, EventArgs e)
        {
            if (cboKho.EditValue != null)
            {
                var maKP = ";" + cboKho.EditValue.ToString() + ";";
                QLBV_Database.QLBVEntities _dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                var thuoc = (from tenduoc in _dataContext.DichVus.Where(p => p.PLoai == 1).Where(p => p.MaKPsd.Contains(maKP))
                             select tenduoc).ToList();
                cboThuoc.Properties.DataSource = thuoc;
            }
            else
            {
                cboThuoc.Properties.DataSource = null;
            }
        }
    }
}
