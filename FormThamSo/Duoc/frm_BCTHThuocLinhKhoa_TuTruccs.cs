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
    public partial class frm_BCTHThuocLinhKhoa_TuTruccs : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCTHThuocLinhKhoa_TuTruccs()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCTHThuocLinhKhoa_TuTruccs_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = System.DateTime.Now;
            lupngayden.DateTime = System.DateTime.Now;            
            List<KPhong> _lkp = new List<KPhong>();
            int x = 0;
            KPhong them = new KPhong();
            them.MaKP = 0;
            them.TenKP = "Tất cả";
            _lkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc || (DungChung.Bien.MaBV == "30009" ? p.PLoai == DungChung.Bien.st_PhanLoaiKP.CanLamSang : x==0 ) ).ToList();
            _lkp.Add(them);
            lupKhoaphong1.Properties.DataSource = _lkp;
            List<KPhong> _lkp1 = new List<KPhong>();
            _lkp1 = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            _lkp1.Add(them);
            lupKhoaphong.Properties.DataSource = _lkp1;
            if(DungChung.Bien.MaBV == "24012")
            {
                ckMau2.Visible = true;
            }
        }
        private bool kt ()
        {
            if (lupKhoaphong.EditValue == null)
            {
                MessageBox.Show("Thiếu kho xuất!", "Thông báo!");
                return false;
            }
            if (lupKhoaphong1.EditValue == null)
            {
                MessageBox.Show("Thiếu khoa|tủ trực!", "Thông báo!");
                return false;
            }
            return true;
        }
        private void bttimkiem_Click(object sender, EventArgs e)
        {
            if(kt())
            {
                DateTime ngaytu = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                DateTime ngayden = DungChung.Ham.NgayDen(lupngayden.DateTime);
                int makp = 0;
                if (lupKhoaphong.EditValue != null)
                {                                                   
                    makp = Convert.ToInt32(lupKhoaphong.EditValue);
                } int makp1 = 0;
                if (lupKhoaphong1.EditValue != null)
                {
                    makp1 = Convert.ToInt32(lupKhoaphong1.EditValue);
                }
                BaoCao.Rep_BCTHThuocLinhKhoa_TuTruc rep = new BaoCao.Rep_BCTHThuocLinhKhoa_TuTruc();
                
                rep.ngathang1.Value = "Từ ngày: " + DungChung.Ham.NgaySangChu(lupNgaytu.DateTime) + " Đến ngày: " + DungChung.Ham.NgaySangChu(lupngayden.DateTime);
                if (ckMau2.Checked)
                {
                    rep.khoxuat.Value = "Kho xuất: " + lupKhoaphong.Text;

                    rep.SubBand_PageHeader_Chung.Visible = false;
                    rep.SubBand_Detail_Chung.Visible = false;
                    rep.SubBand_GroupFooter_Chung.Visible = false;

                    rep.SubBand_PageHeader_Mau2.Visible = true;
                    rep.SubBand_Detail_Mau2.Visible = true;
                    rep.SubBand_GroupFooter_Mau2.Visible = true;

                    var dthuoc = (from f in data.DThuoccts.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => (p.MaKP == makp1 || makp1 == 0) && (p.MaKXuat == makp || makp == 0)
                                                                 && (dkxuat.SelectedIndex == 0 ? p.Status == 1 : (dkxuat.SelectedIndex == 1 ? p.Status == 0 : (p.Status == 0 || p.Status == 1))))
                                  join c in data.DThuocs.Where(p => p.KieuDon == 3) on f.IDDon equals c.IDDon
                                  join d in data.DichVus on f.MaDV equals d.MaDV
                                  select new { f, c, d }).ToList();
                    var hd1 = (from a in dthuoc
                               group a by new
                               {
                                   MaDV = a.d.MaDV,
                                   TenDV = a.d.TenDV,
                                   DonVi = a.d.DonVi,
                                   DonGia = a.f.DonGia
                               } into kq
                               select new
                               {
                                   MaDV = kq.Key.MaDV,
                                   TenDV = kq.Key.TenDV,
                                   DonVi = kq.Key.DonVi,
                                   DonGia = kq.Key.DonGia,
                                   SoLuongX = (from a in kq select a.f.SoLuong).Sum(),
                                   ThanhTienX = (from a in kq select a.f.ThanhTien).Sum()
                               }
                               ).OrderBy(p => p.TenDV).ToList();
                    rep.DataSource = hd1.ToList();
                }
                else
                {
                    //var hd = (from a in data.NhapDs.Where(p => p.PLoai == 2 && (p.KieuDon == 1 || p.KieuDon == 6 || p.KieuDon == 7)).Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => p.MaKP == makp && p.MaKPnx == makp1)
                    //          join b in data.NhapDcts on a.IDNhap equals b.IDNhap
                    //          join f in data.DThuoccts on a.SoPL equals f.SoPL
                    //          join c in data.DThuocs.Where(p => p.KieuDon == 3) on f.IDDon equals c.IDDon
                    //          join d in data.DichVus on b.MaDV equals d.MaDV
                    //          select new { a.SoPL, a.NgayNhap, b.MaDV, d.TenDV, d.DonVi, b.DonGia, b.SoLuongX, b.ThanhTienX }).OrderBy(p => p.NgayNhap).ToList();

                    //var nhapd = (from a in data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => (p.MaKP == makp || makp == 0) && (p.MaKPnx == makp1 || makp1 == 0))
                    //             join b in data.NhapDcts on a.IDNhap equals b.IDNhap
                    //             join d in data.DichVus on b.MaDV equals d.MaDV
                    //             select new { a, b, d }).ToList();
                    var dthuoc = (from f in data.DThuoccts.Where(p => p.NgayNhap >= ngaytu && p.NgayNhap <= ngayden).Where(p => (p.MaKP == makp1 || makp1 == 0) && (p.MaKXuat == makp || makp == 0))
                                  join c in data.DThuocs.Where(p => p.KieuDon == 3) on f.IDDon equals c.IDDon
                                  join d in data.DichVus on f.MaDV equals d.MaDV
                                  select new { f, c, d }).ToList();
                    var hd1 = (from a in dthuoc
                               select new
                               {
                                   SoPL = a.f.SoPL,
                                   NgayNhap = a.f.NgayNhap,
                                   MaDV = a.d.MaDV,
                                   TenDV = a.d.TenDV,
                                   DonVi = a.d.DonVi,
                                   DonGia = a.f.DonGia,
                                   SoLuongX = a.f.SoLuong,
                                   ThanhTienX = a.f.ThanhTien,
                                   a.f.Status
                               }).OrderBy(p => p.NgayNhap).ToList();
                    rep.DataSource = hd1.Where(p => dkxuat.SelectedIndex == 0 ? p.Status == 1 : (dkxuat.SelectedIndex == 1 ? p.Status == 0 : (p.Status == 0 || p.Status == 1))).ToList();
                }
                rep.BindingData();
                rep.CreateDocument();
                frmIn frm = new frmIn();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }
    }
}