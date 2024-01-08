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
    public partial class frm_BCCTTHuocKD : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCCTTHuocKD()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        
        private void frm_BCCTTHuocKD_Load(object sender, EventArgs e)
        {
            datTuNgay.DateTime = DateTime.Now;
            datDenNgay.DateTime = DateTime.Now;
            var khoake = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.PhongKham || p.PLoai == DungChung.Bien.st_PhanLoaiKP.LamSang || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
            var kho = _data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc || p.PLoai == DungChung.Bien.st_PhanLoaiKP.TuTruc).ToList();
            KPhong moi1 = new KPhong();
            moi1.MaKP = 0;
            moi1.TenKP = "Tất cả";
            khoake.Add(moi1);
            kho.Add(moi1);
            bindingSource1.DataSource = khoake.OrderBy(p => p.MaKP);
            bindingSource2.DataSource = kho.OrderBy(p => p.MaKP);
            lupKhoXuat.Properties.DataSource = bindingSource2;
            lupKhoaKe.Properties.DataSource = bindingSource1;
            var tieunhom = _data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 20).ToList();
            TieuNhomDV moi = new TieuNhomDV();
            moi.IdTieuNhom = 0;
            moi.TenTN = "Tất cả";
            tieunhom.Add(moi);
            bindingSource3.DataSource = tieunhom.OrderBy(p => p.IdTieuNhom).ToList();
            var dv = (from a in tieunhom
                      join b in _data.DichVus on a.IdTieuNhom equals b.IdTieuNhom
                      select b).ToList();
            DichVu moi2 = new DichVu();
            moi2.MaDV = 0;
            moi2.TenDV = "Tất cả";
            dv.Add(moi2);
            bindingSource4.DataSource = dv.ToList();
            lupNhomDV.Properties.DataSource = bindingSource3;
            lupDV.Properties.DataSource = bindingSource4;
            radLinh.SelectedIndex = 2;
            radNoiNgoaiTru.SelectedIndex = 2;
            radChiPhi.SelectedIndex = 3;
        }
        List<BaoCao.rep_BCChiTietThuocKeDon.CTThuocKe> _lds = new List<BaoCao.rep_BCChiTietThuocKeDon.CTThuocKe>();
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(datTuNgay.DateTime);
            DateTime dengay = DungChung.Ham.NgayDen(datDenNgay.DateTime);

            int makho = 0; int makhoa = 0; int idtn = 0; int iddv = 0;
            if (lupKhoaKe.Text != "" && lupKhoaKe.Text != "Tất cả")
                makhoa = Convert.ToInt32(lupKhoaKe.EditValue);
            if (lupKhoXuat.Text != "" && lupKhoXuat.Text != "Tất cả")
                makho = Convert.ToInt32(lupKhoXuat.EditValue);
            if (lupNhomDV.Text != "" && lupNhomDV.Text != "Tất cả")
                idtn = Convert.ToInt32(lupNhomDV.EditValue);
            if (lupDV.Text != "" && lupDV.Text != "Tất cả")
                iddv = Convert.ToInt32(lupDV.EditValue);

            var bn = _data.BenhNhans.Where(p => radNoiNgoaiTru.SelectedIndex == 1 ? p.NoiTru == 0 : (radNoiNgoaiTru.SelectedIndex == 0 ? p.NoiTru == 1 : true)).ToList();

            var dv = (from a in _data.DichVus.Where(p => p.PLoai == 1).Where(p => p.MaDV == iddv || iddv == 0)
                      join b in _data.NhomDVs on a.IDNhom equals b.IDNhom
                      join c in _data.TieuNhomDVs.Where(p => p.IdTieuNhom == idtn || idtn == 0) on a.IdTieuNhom equals c.IdTieuNhom
                      select new {a.MaDV, a.TenDV }).ToList();

            var dt1 = (from a in _data.DThuocs.Where(p => p.NgayKe >= tungay && p.NgayKe <= dengay).Where(p => (p.MaKP == makhoa || makhoa == 0) && (p.MaKXuat == makho || makho ==  0))
                       join b in _data.DThuoccts.Where(p => radLinh.SelectedIndex == 0 ? (p.Status == 0) : (radLinh.SelectedIndex == 1 ? (p.Status == 1) : true)).Where(p => radChiPhi.SelectedIndex == 0 ? ((p.AttachIDDonct == null || p.AttachIDDonct < 0) && p.TrongBH == 1) : (radChiPhi.SelectedIndex == 1 ? ((p.AttachIDDonct == null || p.AttachIDDonct < 0) && p.TrongBH == 0) : (radChiPhi.SelectedIndex == 2 ? p.AttachIDDonct > 0 : true))) on a.IDDon equals b.IDDon
                       select new { a.NgayKe, a.MaBNhan, b.MaDV, b.DonGia, b.SoLuong, b.ThanhTien, b.SoPL }).ToList();

            var dt = (from a in dt1
                      join c in bn on a.MaBNhan equals c.MaBNhan 
                      select new { a.NgayKe, c.MaBNhan, c.TenBNhan , a.MaDV, a.DonGia, a.SoLuong, a.ThanhTien, a.SoPL }).ToList();
            if(radNoiNgoaiTru.SelectedIndex == 2 )
            {
                dt = (from a in dt1
                      join c in bn on a.MaBNhan equals c.MaBNhan into k
                      from k1 in k.DefaultIfEmpty()
                      select new { a.NgayKe, MaBNhan = k1 != null ? k1.MaBNhan : 0, TenBNhan = k1 != null ? k1.TenBNhan : "", a.MaDV, a.DonGia, a.SoLuong, a.ThanhTien, a.SoPL }).ToList();
            }
            var ds = (from a in dt
                      join b in dv on a.MaDV equals b.MaDV
                      select new {a.MaDV, b.TenDV, NgayKe = a.NgayKe.Value.Date, SoPL = Convert.ToString(a.SoPL), a.MaBNhan, a.TenBNhan, a.DonGia, a.SoLuong, a.ThanhTien }).OrderBy(p => p.NgayKe).ThenBy(p => p.MaBNhan).OrderBy(p => p.TenDV).ThenBy(p => p.NgayKe).ThenBy(p => p.MaBNhan).ToList();
            
            ds = (from a in ds
                  group a by new { a.MaDV, a.TenDV, a.NgayKe, a.MaBNhan, a.TenBNhan, a.DonGia } into kq
                  select new { kq.Key.MaDV, kq.Key.TenDV, kq.Key.NgayKe, SoPL = string.Join(";",kq.Select(p => p.SoPL)), kq.Key.MaBNhan, kq.Key.TenBNhan, kq.Key.DonGia, SoLuong = kq.Sum(p => p.SoLuong), ThanhTien = kq.Sum(p => p.ThanhTien) }).OrderBy(p => p.TenDV).ThenBy(p => p.NgayKe).ThenBy(p => p.MaBNhan).ToList();
            _lds = (from ds1 in ds.Where(p => p.SoLuong > 0)
                    select new BaoCao.rep_BCChiTietThuocKeDon.CTThuocKe
                    {
                        MaDV = ds1.MaDV??0,
                        TenDV = ds1.TenDV,
                        SoPL = ds1.SoPL,
                        NgayKe = ds1.NgayKe,
                        MaBNhan = ds1.MaBNhan,
                        TenBNhan = ds1.TenBNhan,
                        DonGia = ds1.DonGia,
                        SoLuong = ds1.SoLuong,
                        ThanhTien = ds1.ThanhTien
                    }).ToList();

            BaoCao.rep_BCChiTietThuocKeDon rep = new BaoCao.rep_BCChiTietThuocKeDon(_lds);
            rep.PhanTrang.Value = PhanTrang.Checked;
            rep.ngaythang.Text = "Từ ngày " + tungay.Day + "/" + tungay.Month + "/" + tungay.Year + " đến " + dengay.Day + "/" + dengay.Month + "/" + dengay.Year;
            if (lupKhoaKe.Text != "" && lupKhoaKe.Text != "Tất cả")
                rep.celKhoKeDon.Text = lupKhoaKe.Text;
            else
                rep.celKhoKeDon.Text = "";
            if (lupKhoXuat.Text != "" && lupKhoXuat.Text != "Tất cả")
                rep.celKhoaXuat.Text = lupKhoXuat.Text;
            else
                rep.celKhoaXuat.Text = "";
            rep.DataSource = ds.Where(p => p.SoLuong > 0).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lupNhomDV_EditValueChanged(object sender, EventArgs e)
        {
            if (lupNhomDV.Text == "Tất cả" || lupNhomDV.Text == "")
            {
                var tieunhom = _data.TieuNhomDVs.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10 || p.IDNhom == 11 || p.IDNhom == 20).ToList();
                var dv = (from a in tieunhom
                          join b in _data.DichVus on a.IdTieuNhom equals b.IdTieuNhom
                          select b).ToList();
                DichVu moi2 = new DichVu();
                moi2.MaDV = 0;
                moi2.TenDV = "Tất cả";
                dv.Add(moi2);
                bindingSource4.DataSource = dv.OrderBy(p => p.MaDV).ToList();
                lupDV.Properties.DataSource = bindingSource4;
            }
            else
            {
                int x = Convert.ToInt32(lupNhomDV.EditValue);
                var dv = (from b in _data.DichVus.Where(p => p.IdTieuNhom == x)
                          select b).ToList();
                DichVu moi2 = new DichVu();
                moi2.MaDV = 0;
                moi2.TenDV = "Tất cả";
                dv.Add(moi2);
                bindingSource4.DataSource = dv.OrderBy(p => p.MaDV).ToList();
                lupDV.Properties.DataSource = bindingSource4;
            }
        }

    }
}