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
    public partial class frm_BC_XuatKhangSinh : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_XuatKhangSinh()
        {
            InitializeComponent();
        }
        #region class kho dược
        private class KhoDuoc
        {
            public bool Chon { get; set; }
            public int MaKP { get; set; }
            public string TenKP { get; set; }
        }
        #endregion
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KhoDuoc> _lKhoDuoc = new List<KhoDuoc>();
        List<DungChung.Bien.c_PhanLoaiXuat> dsPLXuat = new List<DungChung.Bien.c_PhanLoaiXuat>();
        private void frm_BC_XuatKhangSinh_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = DateTime.Now;
            dtDenNgay.DateTime = DateTime.Now;
            //List<DungChung.Bien.c_PhanLoaiXuat> dsPLXuat = new List<DungChung.Bien.c_PhanLoaiXuat>();
            dsPLXuat = DungChung.Bien.c_PhanLoaiXuat._setPhanLoaiXuat();
            dsPLXuat.Add(new DungChung.Bien.c_PhanLoaiXuat { Id = -1, PhanLoai = "Tất cả", Check = true });
            grcPLXuat.DataSource = dsPLXuat.OrderBy(p => p.Id).ToList();

            var qkho = (from kp in data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc) select new { kp.MaKP, kp.TenKP });
            if (qkho.Count() > 0)
            {
                KhoDuoc moi = new KhoDuoc();
                moi.Chon = true;
                moi.MaKP = 0;
                moi.TenKP = "Tất cả";
                _lKhoDuoc.Add(moi);
                foreach (var item in qkho)
                {
                    KhoDuoc moi1 = new KhoDuoc();
                    moi1.Chon = true;
                    moi1.MaKP = item.MaKP;
                    moi1.TenKP = item.TenKP;
                    _lKhoDuoc.Add(moi1);
                }
                grcKhoaphong.DataSource = _lKhoDuoc.ToList();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(dtTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(dtDenNgay.DateTime);
            List<KhoDuoc> dskhochon = new List<KhoDuoc>();
            dskhochon = _lKhoDuoc.Where(p => p.Chon == true && p.MaKP > 0).ToList();
            List<int> plxuat = new List<int>();
            plxuat = dsPLXuat.Where(p => p.Check == true).Select(p => p.Id).ToList();
            //for (int i = 0; i < cklPLoaiXuat.ItemCount; i++)
            //{
            //    if (cklPLoaiXuat.GetItemCheckState(i) == CheckState.Checked)
            //    {
            //        plxuat.Add(Convert.ToInt32(cklPLoaiXuat.GetItemValue(i)));
            //    }
            //}

            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1).Where(p => DungChung.Bien.MaBV == "30007" ? true : (p.DuongD.ToLower().Contains("uống") || p.DuongD.ToLower().Contains("tiêm")))
                       join tn in data.TieuNhomDVs.Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuocThuong_khangsinh) on dv.IdTieuNhom equals tn.IdTieuNhom
                       join dvex in data.DichVuExes on dv.MaDV equals dvex.MaDV into q
                       from q1 in q.DefaultIfEmpty()
                       select new
                       {
                           dv.SoTTqd,
                           dv.MaDV,
                           dv.TenHC,
                           q1.MaATC,
                           dv.SoTT,
                           dv.TenDV,
                           dv.NuocSX,
                           dv.HamLuong,
                           dv.DonVi,
                           dv.DuongD
                       }).Where(p => DungChung.Bien.MaBV == "30007" ? true : p.DonVi.ToLower().Contains("chai") || p.DonVi.ToLower().Contains("lọ") || p.DonVi.ToLower().Contains("ống") || p.DonVi.ToLower().Contains("viên") || p.DonVi.ToLower().Contains("túi")).ToList();
            var qnhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          select new { ndct.MaDV, ndct.SoLuongX, ndct.DonGia, nd.KieuDon, ndct.SoLuongN, nd.MaKP }).ToList();
            
            if (DungChung.Bien.MaBV == "27023")
            {
                qnhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 2 || (p.PLoai == 1 && p.KieuDon == 2)).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          select new { ndct.MaDV, ndct.SoLuongX, ndct.DonGia, nd.KieuDon, ndct.SoLuongN, nd.MaKP }).ToList();
            }

            var query = (from a in qdv
                         join b in qnhapd on a.MaDV equals b.MaDV
                         join c in plxuat on b.KieuDon equals c
                         join d in dskhochon on b.MaKP equals d.MaKP
                         group new { a, b } by new { a.SoTTqd, a.TenHC, a.MaATC, a.SoTT, a.TenDV, a.NuocSX, a.HamLuong, a.DuongD, a.DonVi, b.DonGia } into kq
                         select new
                         {
                             kq.Key.SoTTqd,
                             TenHC = kq.Key.TenHC == null ? "" : kq.Key.TenHC,
                             kq.Key.MaATC,
                             kq.Key.SoTT,
                             kq.Key.TenDV,
                             kq.Key.NuocSX,
                             HamLuong = kq.Key.HamLuong == null ? "" : kq.Key.HamLuong,
                             kq.Key.DuongD,
                             kq.Key.DonVi,
                             SoLuong = DungChung.Bien.MaBV == "27023" ? (kq.Sum(p => p.b.SoLuongX) - kq.Sum(p => p.b.SoLuongN)) : kq.Sum(p => p.b.SoLuongX),
                             kq.Key.DonGia
                         }).Where(p => p.TenHC != "" && p.HamLuong != "").ToList();

            BaoCao.Rep_BC_XuatKhangSinh_30007 rep = new BaoCao.Rep_BC_XuatKhangSinh_30007();
            frmIn frm = new frmIn();
            rep.DataSource = query.OrderBy(p => p.TenHC).ToList();
            rep.celTuNgay.Text = "(Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy") + ")";
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void cklPLoaiXuat_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            //if (cklPLoaiXuat.CheckedItems == CheckState.Checked)
            //{
            //    for (int i = 0; i < cklPLoaiXuat.ItemCount; i++)
            //    {
            //        cklPLoaiXuat.SetItemChecked(i, true);
            //    }
            //}
            //else if (cklPLoaiXuat.GetItemCheckState(0) == CheckState.Unchecked)
            //{
            //    for (int i = 0; i < cklPLoaiXuat.ItemCount; i++)
            //    {
            //        cklPLoaiXuat.SetItemChecked(i, false);
            //    }
            //}
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("TenKP") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("TenKP").ToString();

                    if (Ten == "Tất cả")
                    {
                        if (_lKhoDuoc.First().Chon == true)
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _lKhoDuoc)
                            {
                                a.Chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _lKhoDuoc.ToList();
                    }
                }
            }
        }

        private void grvPLXuat_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colChon1")
            {
                if (grvPLXuat.GetFocusedRowCellValue("PhanLoai") != null)
                {
                    string Ten = grvPLXuat.GetFocusedRowCellValue("PhanLoai").ToString();

                    if (Ten == "Tất cả")
                    {
                        if (dsPLXuat.First().Check == true)
                        {
                            foreach (var a in dsPLXuat)
                            {
                                a.Check = false;
                            }
                        }
                        else
                        {
                            foreach (var a in dsPLXuat)
                            {
                                a.Check = true;
                            }
                        }
                        grcPLXuat.DataSource = "";
                        grcPLXuat.DataSource = dsPLXuat.OrderBy(p => p.Id).ToList();
                    }
                }
            }
        }
    }
}