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
    public partial class frm_BC_THBNThanhToanRaVien_27023 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_THBNThanhToanRaVien_27023()
        {
            InitializeComponent();
        }

        private void frm_BC_THBNThanhToanRaVien_27023_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            rgTrangThai.SelectedIndex = 2;
            rdNoiNgoaiTru.SelectedIndex = 2;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<MucTT> _listmuc = new List<MucTT>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            if (rdLoaiNgay.SelectedIndex == 1)
            {
                var vphi0 = (from vp in data.VienPhis.Where(p => p.NgayDuyet >= tungay && p.NgayDuyet <= denngay).Where(p => rgTrangThai.SelectedIndex == 2 ? true : (rgTrangThai.SelectedIndex == 0 ? p.NgayDuyet == null : p.NgayDuyet != null))
                             join vpct in data.VienPhicts.Where(p => p.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                             join bn in data.BenhNhans.Where(p => rdNoiNgoaiTru.SelectedIndex == 2 ? true : p.NoiTru == rdNoiNgoaiTru.SelectedIndex).Where(p => p.MaKCB == DungChung.Bien.MaBV).Where(p => p.DTuong.Contains("BHYT") || p.DTuong.ToLower().Contains("dịch vụ")) on vp.MaBNhan equals bn.MaBNhan
                             where (bn.DTuong.Contains("BHYT") ? vpct.TrongBH == 1 : vpct.TrongBH == 0)
                             select new { bn.MaBNhan, vpct.TienBN, vpct.TyLeBHTT, bn.TenBNhan, bn.DChi }).ToList();

                var vphi = (from vp in vphi0
                            group vp by new { vp.MaBNhan, vp.TenBNhan, vp.DChi, vp.TyLeBHTT } into kq
                            select new { kq.Key.MaBNhan, ThanhToan = kq.Sum(p => p.TienBN), kq.Key.TyLeBHTT, kq.Key.TenBNhan, kq.Key.DChi }).ToList();

                List<int> _arrmabn = vphi.Select(p => p.MaBNhan).Distinct().ToList();
                var qtamung = (from tu in data.TamUngs.Where(p => _arrmabn.Contains(p.MaBNhan ?? 0)).Where(p=>p.PhanLoai==0|| p.PhanLoai == 3)
                               group tu by new { tu.MaBNhan, tu.PhanLoai } into kq
                               select new
                               {
                                   kq.Key.PhanLoai,
                                   kq.Key.MaBNhan,
                                   TienUng = kq.Key.PhanLoai == 0 ? kq.Sum(p => p.SoTien) : 0,
                                   ThuThang = kq.Key.PhanLoai == 3 ? kq.Sum(p => p.SoTien) : 0,
                               }).ToList();
                var qvp = (from vp in vphi
                           join tu in qtamung on vp.MaBNhan equals tu.MaBNhan into k1
                           from k in k1.DefaultIfEmpty()
                           select new
                           {
                               vp.MaBNhan,
                               vp.TenBNhan,
                               vp.DChi,
                               DT = (k != null && k.PhanLoai == 3) ? "t" : ((100 - vp.TyLeBHTT) + "%"),
                               TienUng = (k != null) ? (k.PhanLoai == 3 ? 0 : k.TienUng) : 0,
                               ThanhToan = (k != null) ? (k.PhanLoai == 3 ? 0 : vp.ThanhToan) : vp.ThanhToan,
                               ThuThang = (k != null) ? (k.PhanLoai == 3 ? k.ThuThang : 0) : 0
                           }).ToList();
                var query = (from b in qvp
                             group b by new { b.MaBNhan, b.TenBNhan, b.DChi, b.DT } into kq
                             select new BNTTRaVien
                             {
                                 TenBNhan = kq.Key.TenBNhan,
                                 DChi = kq.Key.DChi,
                                 DT = kq.Key.DT,
                                 TienUng = kq.Key.DT == "t" ? null : (kq.Sum(p => p.TienUng) == 0 ? null : kq.Sum(p => p.TienUng)),
                                 ThanhToan = kq.Key.DT == "t" ? kq.Sum(p => p.ThuThang) : kq.Sum(p => p.ThanhToan),
                                 ChiTra = kq.Key.DT == "t" ? null : (((kq.Sum(p => p.TienUng) - kq.Sum(p => p.ThanhToan)) < 0) ? null : kq.Sum(p => p.TienUng) - kq.Sum(p => p.ThanhToan)),
                                 ThuThieu = kq.Key.DT == "t" ? null : (((kq.Sum(p => p.ThanhToan) - kq.Sum(p => p.TienUng)) < 0) ? null : kq.Sum(p => p.ThanhToan) - kq.Sum(p => p.TienUng))
                             }).ToList();

                BaoCao.Rep_BC_THBNThanhToanRaVien_27023 rep = new BaoCao.Rep_BC_THBNThanhToanRaVien_27023();
                frmIn frm = new frmIn();
                rep.celTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = query.Where(p => p.TienUng > 0 || p.ThanhToan > 0).OrderBy(p => p.TenBNhan).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                var vphi0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay).Where(p => rgTrangThai.SelectedIndex == 2 ? true : (rgTrangThai.SelectedIndex == 0 ? p.NgayDuyet == null : p.NgayDuyet != null))
                             join vpct in data.VienPhicts.Where(p => p.ThanhToan == 0) on vp.idVPhi equals vpct.idVPhi
                             join bn in data.BenhNhans.Where(p => rdNoiNgoaiTru.SelectedIndex == 2 ? true : p.NoiTru == rdNoiNgoaiTru.SelectedIndex).Where(p => p.MaKCB == DungChung.Bien.MaBV).Where(p => p.DTuong.Contains("BHYT") || p.DTuong.ToLower().Contains("dịch vụ")) on vp.MaBNhan equals bn.MaBNhan
                             where (bn.DTuong.Contains("BHYT") ? vpct.TrongBH == 1 : vpct.TrongBH == 0)
                             select new { bn.MaBNhan, vpct.TienBN, vpct.TyLeBHTT, bn.TenBNhan, bn.DChi }).ToList();

                var vphi = (from vp in vphi0
                            group vp by new { vp.MaBNhan, vp.TenBNhan, vp.DChi, vp.TyLeBHTT } into kq
                            select new { kq.Key.MaBNhan, ThanhToan = kq.Sum(p => p.TienBN), kq.Key.TyLeBHTT, kq.Key.TenBNhan, kq.Key.DChi }).ToList();

                List<int> _arrmabn = vphi.Select(p => p.MaBNhan).Distinct().ToList();
                var qtamung = (from tu in data.TamUngs.Where(p => _arrmabn.Contains(p.MaBNhan ?? 0)).Where(p => p.PhanLoai == 0 || p.PhanLoai == 3)
                               group tu by new { tu.MaBNhan, tu.PhanLoai } into kq
                               select new
                               {
                                   kq.Key.PhanLoai,
                                   kq.Key.MaBNhan,
                                   TienUng = kq.Key.PhanLoai == 0 ? kq.Sum(p => p.SoTien) : 0,
                                   ThuThang = kq.Key.PhanLoai == 3 ? kq.Sum(p => p.SoTien) : 0,
                               }).ToList();
                var qvp = (from vp in vphi
                           join tu in qtamung on vp.MaBNhan equals tu.MaBNhan into k1
                           from k in k1.DefaultIfEmpty()
                           select new
                           {
                               vp.MaBNhan,
                               vp.TenBNhan,
                               vp.DChi,
                               DT = (k != null && k.PhanLoai == 3) ? "t" : ((100 - vp.TyLeBHTT) + "%"),
                               TienUng = (k != null) ? (k.PhanLoai == 3 ? 0 : k.TienUng) : 0,
                               ThanhToan = (k != null) ? (k.PhanLoai == 3 ? 0 : vp.ThanhToan) : vp.ThanhToan,
                               ThuThang = (k != null) ? (k.PhanLoai == 3 ? k.ThuThang : 0) : 0
                           }).ToList();
                var query = (from b in qvp
                             group b by new { b.MaBNhan, b.TenBNhan, b.DChi, b.DT } into kq
                             select new BNTTRaVien
                             {
                                 TenBNhan = kq.Key.TenBNhan,
                                 DChi = kq.Key.DChi,
                                 DT = kq.Key.DT,
                                 TienUng = kq.Key.DT == "t" ? null : (kq.Sum(p => p.TienUng) == 0 ? null : kq.Sum(p => p.TienUng)),
                                 ThanhToan = kq.Key.DT == "t" ? kq.Sum(p => p.ThuThang) : kq.Sum(p => p.ThanhToan),
                                 ChiTra = kq.Key.DT == "t" ? null : (((kq.Sum(p => p.TienUng) - kq.Sum(p => p.ThanhToan)) < 0) ? null : kq.Sum(p => p.TienUng) - kq.Sum(p => p.ThanhToan)),
                                 ThuThieu = kq.Key.DT == "t" ? null : (((kq.Sum(p => p.ThanhToan) - kq.Sum(p => p.TienUng)) < 0) ? null : kq.Sum(p => p.ThanhToan) - kq.Sum(p => p.TienUng))
                             }).ToList();

                BaoCao.Rep_BC_THBNThanhToanRaVien_27023 rep = new BaoCao.Rep_BC_THBNThanhToanRaVien_27023();
                frmIn frm = new frmIn();
                rep.celTuNgay.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                rep.DataSource = query.Where(p => p.TienUng > 0 || p.ThanhToan > 0).OrderBy(p => p.TenBNhan).ToList();
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private class BNTTRaVien
        {
            public string TenBNhan { get; set; }
            public string DChi { get; set; }
            public string DT { get; set; }
            public double? TienUng { get; set; }
            public double? ThanhToan { get; set; }
            public double? ChiTra { get; set; }
            public double? ThuThieu { get; set; }
        }
    }
}