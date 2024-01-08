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
    public partial class frm_SoTheoDoi_27022 : DevExpress.XtraEditors.XtraForm
    {
        public frm_SoTheoDoi_27022()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ktcd()
        {
            if (lupNgaytu.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Ngày từ");
                lupNgaytu.Focus();
                return false;
            }
            if (lupngayden.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Ngày đến");
                lupngayden.Focus();
                return false;
            }
            if (lupKhoa.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho dược");
                lupKhoa.Focus();
                return false;
            }
            return true;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ktcd())
            {
                DateTime tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
                DateTime denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
                DateTime tungay1 = DungChung.Ham.NgayTu(lupNgaytu.DateTime).AddMonths(-2);
                DateTime denngay1 = DungChung.Ham.NgayDen(lupngayden.DateTime).AddMonths(2);
                int makp = 0;
                if (lupKhoa.Text != "" && lupKhoa.Text != "Tất cả")
                {
                    makp = Convert.ToInt32(lupKhoa.EditValue.ToString());
                }
                var dv = (from a in data.DichVus.Where(p => p.PLoai == 1)
                          join b in data.NhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6) on a.IDNhom equals b.IDNhom
                          select a).ToList();
                var dt = (from a in data.DThuocs.Where(p => p.PLDV == 1).Where(p => p.NgayKe >= tungay1 && p.NgayKe <= denngay1).Where(p => p.MaKXuat == makp || makp == 0)
                          join b in data.DThuoccts.Where(p => p.Status == 1) on a.IDDon equals b.IDDon
                          select new
                          {
                              a.MaBNhan,
                              a.NgayKe,
                              b.MaCB,
                              b.MaDV,
                              b.SoLuong,
                              CachDung = (b.DuongD??"") + " " + (b.SoLan??"") + " " + (b.MoiLan??"") + " " + (b.Luong??"") + " " + (b.DviUong??"") + ", " + b.GhiChu,
                              a.IDDon,
                              a.MaKP,
                              
                          }).ToList();
                var nd = (from a in data.NhapDs.Where(p => p.PLoai == 2 && p.KieuDon == 0 && p.XuatTD == 1).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.MaBNhan != null && p.MaBNhan != 0)
                          select new
                          {
                              a.MaBNhan,
                              MaDV = 0,
                              a.NgayNhap,
                              a.SoCT
                          }).ToList();
                List<NhapD> _nd = new List<NhapD>();
                foreach (var item in nd)
                {
                    string[] arr = item.SoCT.Split(';');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        NhapD moi = new NhapD();
                        moi.MaBNhan = item.MaBNhan;
                        moi.NgayNhap = item.NgayNhap;
                        moi.SoPL = Convert.ToInt32(arr[i]);
                        _nd.Add(moi);
                    }
                }
                var ds1 = (from a in dt
                           join b in _nd on a.IDDon equals b.SoPL
                           where (a.MaBNhan == b.MaBNhan)
                           select new
                           {
                               a.MaBNhan,
                               a.NgayKe,
                               a.MaCB,
                               a.MaDV,
                               a.SoLuong,
                               a.CachDung,
                               b.NgayNhap,
                               a.MaKP,
                               
                           }).ToList();
                var bnkb = (from a in ds1
                            join b in data.BNKBs on a.MaBNhan equals b.MaBNhan
                            where (a.MaKP == b.MaKP)
                            group new { a, b } by a into kq
                            select new { kq.Key, idkb = kq.Where(p => p.a.NgayKe >= p.b.NgayKham).Count() > 0 ? kq.Where(p => p.a.NgayKe >= p.b.NgayKham).Max(p => p.b.IDKB) : 0 }).ToList();
                var ds = (from a in bnkb
                          join b in dv on a.Key.MaDV equals b.MaDV
                          join c in data.BNKBs on a.idkb equals c.IDKB
                          join d in data.BenhNhans.Where(p => p.NoiTru == 0 && p.DTNT == false) on a.Key.MaBNhan equals d.MaBNhan
                          join f in data.CanBoes on a.Key.MaCB equals f.MaCB
                          select new
                          {
                              
                              a.Key.CachDung,
                              a.Key.MaBNhan,
                              a.Key.MaCB,
                              a.Key.MaDV,
                              a.Key.MaKP,
                              a.Key.NgayKe,
                              a.Key.NgayNhap,
                              a.Key.SoLuong,
                              
                              d.TenBNhan,
                              c.ChanDoan,
                              GTinh_Tuoi = (d.GTinh == 0 ? "Nữ, " : "Nam, ") + d.Tuoi,
                              f.TenCB,
                              d.DChi,
                              b.TenDV,
                              TenThuoc = b.TenDV + " - " + b.HamLuong,
                              Thuoc_SoLuong = b.TenDV + " x  [" + a.Key.SoLuong + "]",
                              b.DonVi,
                              b.IdTieuNhom,
                              b.TenHC
                          }).ToList();
                if (chekDonVi.Checked == false)
                {
                    if (rdMau.SelectedIndex == 0)
                    {
                        BaoCao.rep_SoTheoDoiTTBN rep = new BaoCao.rep_SoTheoDoiTTBN();
                        rep.lbl_tungaydenngay.Text = "Từ ngày: " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                        rep.DataSource = ds.OrderBy(p => p.NgayNhap);
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    if (rdMau.SelectedIndex == 1)
                    {
                        BaoCao.rep_SoBanThuocTheoDon rep = new BaoCao.rep_SoBanThuocTheoDon();
                        rep.lbl_tungaydenngay.Text = "Từ ngày: " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                        rep.DataSource = ds.Where(p => !p.TenDV.ToLower().Contains("tobiwel") && !p.TenDV.ToLower().Contains("natri clorid 0,9%") && !p.TenDV.ToLower().Contains("natriclorid 0,9%") & !p.TenDV.ToLower().Contains("kalium") & !p.TenDV.ToLower().Contains("venrutine")).OrderBy(p => p.NgayNhap);
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    if (rdMau.SelectedIndex == 2)
                    {
                        BaoCao.rep_SoBanThuocKhongTheoDon rep = new BaoCao.rep_SoBanThuocKhongTheoDon();
                        rep.lbl_tungaydenngay.Text = "Từ ngày: " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                        rep.DataSource = ds.Where(p => p.TenDV.ToLower().Contains("tobiwel") || p.TenDV.ToLower().Contains("natri clorid 0,9%") || p.TenDV.ToLower().Contains("natriclorid 0,9%") || p.TenDV.ToLower().Contains("kalium") || p.TenDV.ToLower().Contains("venrutine")).OrderBy(p => p.NgayNhap);
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    
                }
                else
                {
                    if (rdMau.SelectedIndex == 0)
                    {
                        BaoCao.rep_SoTheoDoiTTBN_DonVi rep = new BaoCao.rep_SoTheoDoiTTBN_DonVi();
                        rep.lbl_tungaydenngay.Text = "Từ ngày: " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                        rep.DataSource = ds.OrderBy(p => p.NgayNhap);
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    if (rdMau.SelectedIndex == 1)
                    {
                        BaoCao.rep_SoBanThuocTheoDon_DonVi rep = new BaoCao.rep_SoBanThuocTheoDon_DonVi();
                        rep.lbl_tungaydenngay.Text = "Từ ngày: " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                        rep.DataSource = ds.Where(p => !p.TenDV.ToLower().Contains("tobiwel") && !p.TenDV.ToLower().Contains("natri clorid 0,9%") && !p.TenDV.ToLower().Contains("natriclorid 0,9%") & !p.TenDV.ToLower().Contains("kalium") & !p.TenDV.ToLower().Contains("venrutine")).OrderBy(p => p.NgayNhap);
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    if (rdMau.SelectedIndex == 2)
                    {
                        BaoCao.rep_SoBanThuocKhongTheoDon_DonVi rep = new BaoCao.rep_SoBanThuocKhongTheoDon_DonVi();
                        rep.lbl_tungaydenngay.Text = "Từ ngày: " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
                        rep.DataSource = ds.Where(p => p.TenDV.ToLower().Contains("tobiwel") || p.TenDV.ToLower().Contains("natri clorid 0,9%") || p.TenDV.ToLower().Contains("natriclorid 0,9%") || p.TenDV.ToLower().Contains("kalium") || p.TenDV.ToLower().Contains("venrutine")).OrderBy(p => p.NgayNhap);
                        rep.BindingData();
                        rep.CreateDocument();
                        frmIn frm = new frmIn();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
        }
        List<KPhong> _kp = new List<KPhong>();
        private void frm_BCDoiChieuGia_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            var qkp = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            _kp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            foreach(var item in qkp)
            {
                _kp.Insert(0, new KPhong { MaKP = item.MaKP, TenKP = item.TenKP });
            }
            lupKhoa.Properties.DataSource = _kp;

            
        }

    }
}