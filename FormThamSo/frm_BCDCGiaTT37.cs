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
    public partial class frm_BCDCGiaTT37 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCDCGiaTT37()
        {
            InitializeComponent();
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class bc1
        {
            public string STT { get; set; }
            public string NoiDung { get; set; }
            public double SL2017 { get; set; }
            public double TT2017 { get; set; }
            public double SL2018 { get; set; }
            public double TT2018 { get; set; }

        }
        List<bc1> _ds = new List<bc1>();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime ngaytu1 = DungChung.Ham.NgayTu(Convert.ToDateTime("1/1/"+Nam1.Text));
            DateTime ngayden1 = DungChung.Ham.NgayDen(Convert.ToDateTime("31/12/" + Nam1.Text));
            DateTime ngaytu2 = DungChung.Ham.NgayTu(NgayTu2.DateTime);
            DateTime ngayden2 = DungChung.Ham.NgayDen(NgayDen2.DateTime);
            var vp = (from a in _data.VienPhis
                      join b in _data.VienPhicts on a.idVPhi equals b.idVPhi
                      select new {a.MaBNhan, a.NgayTT, a.NgayDuyet,
                          b.MaDV, b.SoLuong, b.TienBH, b.ThanhTien, b.TienBN, b.MaKP
                      }).ToList();
            var dv = (from a in _data.DichVus
                      join b in _data.TieuNhomDVs on a.IdTieuNhom equals b.IdTieuNhom
                      join c in _data.NhomDVs on a.IDNhom equals c.IDNhom
                      select new { a.MaDV, b.TenRG, b.TenTN, a.Loai, c.TenNhom, c.IDNhom}).ToList();
            var tu = (from a in _data.TamUngs.Where(p => p.PhanLoai == 3)
                      join b in _data.TamUngcts on a.IDTamUng equals b.IDTamUng
                      select new { b.MaDV, b.ThanhTien, b.SoLuong, a.PhanLoai, a.NgayThu, a.MaKP, a.MaBNhan }).ToList();
            var ds1 = (from a in vp
                       join b in dv on a.MaDV equals b.MaDV
                       join c in _data.KPhongs on a.MaKP equals c.MaKP
                       join d in _data.BenhNhans on a.MaBNhan equals d.MaBNhan
                       select new { a.MaBNhan, d.DTuong,NgayTT = radioGroup1.SelectedIndex == 0 ? a.NgayTT : a.NgayDuyet, a.MaDV, a.SoLuong, a.TienBH, a.ThanhTien, a.TienBN, b.TenRG, b.TenTN, c.PLoai, b.Loai, b.TenNhom, b.IDNhom }).ToList();

            var ds2 = (from a in tu
                       join b in dv on a.MaDV equals b.MaDV
                       join c in _data.KPhongs on a.MaKP equals c.MaKP
                       join d in _data.BenhNhans on a.MaBNhan equals d.MaBNhan
                       join f in _data.VienPhis on a.MaBNhan equals f.MaBNhan
                       select new { a.MaBNhan, d.DTuong, NgayTT = radioGroup1.SelectedIndex == 0 ? f.NgayTT : f.NgayDuyet, a.MaDV, a.SoLuong, a.ThanhTien, b.TenRG, b.TenTN, c.PLoai, b.Loai, b.TenNhom, b.IDNhom }).ToList();
            _ds.Clear();

            bc1 moi1 = new bc1();
            moi1.STT = "I";
            moi1.NoiDung = "Tổng nguồn thu";
            moi1.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Sum(p => p.SoLuong) + ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Sum(p => p.SoLuong);
            moi1.TT2017 = (ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Sum(p => p.ThanhTien) + ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Sum(p => p.ThanhTien))/1000000;
            moi1.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Sum(p => p.SoLuong) + ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Sum(p => p.SoLuong);
            moi1.TT2018 = (ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Sum(p => p.ThanhTien) + ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Sum(p => p.ThanhTien))/1000000 ;
            _ds.Add(moi1);

            bc1 moi2 = new bc1();
            moi2.STT = "1";
            moi2.NoiDung = "Thu BHYT";
            moi2.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Sum(p => p.SoLuong);
            moi2.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Sum(p => p.ThanhTien)/1000000;
            moi2.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Sum(p => p.SoLuong);
            moi2.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi2);

            bc1 moi3 = new bc1();
            moi3.STT = "a";
            moi3.NoiDung = "Số khám bệnh";
            moi3.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.SoLuong);
            moi3.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.ThanhTien)/1000000;
            moi3.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.SoLuong);
            moi3.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi3);

            bc1 moi4 = new bc1();
            moi4.STT = "";
            moi4.NoiDung = "Tại bệnh viện";
            moi4.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.SoLuong);
            moi4.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.ThanhTien)/1000000;
            moi4.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.SoLuong);
            moi4.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi4);

            bc1 moi5 = new bc1();
            moi5.STT = "";
            moi5.NoiDung = "Tại các trạm y tế";
            moi5.SL2017 = 0;
            moi5.TT2017 = 0;
            moi5.SL2018 = 0;
            moi5.TT2018 = 0;
            _ds.Add(moi5);

            bc1 moi6 = new bc1();
            moi6.STT = "b";
            moi6.NoiDung = "Ngày giường điều trị nội trú";
            moi6.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.SoLuong);
            moi6.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.ThanhTien)/1000000;
            moi6.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.SoLuong);
            moi6.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi6);

            bc1 moi7 = new bc1();
            moi7.STT = "c";
            moi7.NoiDung = "Tổng số phẫu thuật, trong đó:";
            moi7.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi7.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien) / 1000000;
            moi7.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi7.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien) / 1000000;
            _ds.Add(moi7);

            bc1 moi8 = new bc1();
            moi8.STT = "";
            moi8.NoiDung = "- Phẫu thuật đặc biệt";
            moi8.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi8.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            moi8.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi8.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi8);

            bc1 moi9 = new bc1();
            moi9.STT = "";
            moi9.NoiDung = "- Phẫu thuật loại 1";
            moi9.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi9.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            moi9.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi9.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi9);

            bc1 moi10 = new bc1();
            moi10.STT = "";
            moi10.NoiDung = "- Phẫu thuật loại 2";
            moi10.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi10.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            moi10.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi10.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi10);

            bc1 moi11 = new bc1();
            moi11.STT = "";
            moi11.NoiDung = "- Phẫu thuật loại 3";
            moi11.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi11.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            moi11.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi11.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi11);

            bc1 moi12 = new bc1();
            moi12.STT = "d";
            moi12.NoiDung = "Tổng số thủ thuật, trong đó:";
            moi12.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi12.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien) / 1000000;
            moi12.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi12.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien) / 1000000;
            _ds.Add(moi12);

            bc1 moi13 = new bc1();
            moi13.STT = "";
            moi13.NoiDung = "- Thủ thuật đặc biệt";
            moi13.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi13.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            moi13.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi13.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi13);

            bc1 moi14 = new bc1();
            moi14.STT = "";
            moi14.NoiDung = "- Thủ thuật loại 1";
            moi14.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi14.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            moi14.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi14.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi14);

            bc1 moi15 = new bc1();
            moi15.STT = "";
            moi15.NoiDung = "- Thủ thuật loại 2";
            moi15.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi15.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            moi15.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi15.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi15);

            bc1 moi16 = new bc1();
            moi16.STT = "";
            moi16.NoiDung = "- Thủ thuật loại 3";
            moi16.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi16.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            moi16.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi16.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi16);

            bc1 moi17 = new bc1();
            moi17.STT = "e";
            moi17.NoiDung = "Các xét nghiệm, cận lâm sàng";
            moi17.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi17.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            moi17.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi17.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi17);

            bc1 moi18 = new bc1();
            moi18.STT = "";
            moi18.NoiDung = "- Tổng số xét nghiệm huyết học các loại";
            moi18.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.SoLuong);
            moi18.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.ThanhTien)/1000000;
            moi18.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.SoLuong);
            moi18.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi18);

            bc1 moi19 = new bc1();
            moi19.STT = "";
            moi19.NoiDung = "- Tổng số xét nghiệm hóa sinh các loại";
            moi19.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.SoLuong);
            moi19.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.ThanhTien)/1000000;
            moi19.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.SoLuong);
            moi19.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi19);

            bc1 moi20 = new bc1();
            moi20.STT = "";
            moi20.NoiDung = "- Tổng số xét nghiệm vi sinh các loại";
            moi20.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.SoLuong);
            moi20.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.ThanhTien)/1000000;
            moi20.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.SoLuong);
            moi20.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi20);

            bc1 moi21 = new bc1();
            moi21.STT = "";
            moi21.NoiDung = "- Tổng số xét nghiệm giải phẫu các loại";
            moi21.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.SoLuong);
            moi21.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.ThanhTien)/1000000;
            moi21.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.SoLuong);
            moi21.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi21);

            bc1 moi22 = new bc1();
            moi22.STT = "";
            moi22.NoiDung = "- Tổng số lần siêu âm";
            moi22.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.SoLuong);
            moi22.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.ThanhTien)/1000000;
            moi22.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.SoLuong);
            moi22.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi22);

            bc1 moi23 = new bc1();
            moi23.STT = "";
            moi23.NoiDung = "- Tổng số lần chụp XQ các loại";
            moi23.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.SoLuong);
            moi23.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.ThanhTien)/1000000;
            moi23.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.SoLuong);
            moi23.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi23);

            bc1 moi24 = new bc1();
            moi24.STT = "";
            moi24.NoiDung = "- Tổng số lần chụp CT Scanner các loại";
            moi24.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.SoLuong);
            moi24.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.ThanhTien)/1000000;
            moi24.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.SoLuong);
            moi24.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi24);

            bc1 moi25 = new bc1();
            moi25.STT = "";
            moi25.NoiDung = "- Tổng số lần chụp MRI các loại";
            moi25.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi25.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            moi25.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi25.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi25);

            bc1 moi26 = new bc1();
            moi26.STT = "f";
            moi26.NoiDung = "Thuốc, máu, dịch truyền, VTTH, Vật tư thay thế";
            moi26.SL2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.SoLuong);
            moi26.TT2017 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1 && p.TienBH != 0).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.ThanhTien)/1000000;
            moi26.SL2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.SoLuong);
            moi26.TT2018 = ds1.Where(p => p.DTuong == "BHYT").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2 && p.TienBH != 0).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi26);

            bc1 moi27 = new bc1();
            moi27.STT = "g";
            moi27.NoiDung = "Khác";
            moi27.SL2017 = moi2.SL2017 - moi3.SL2017 - moi6.SL2017 - moi7.SL2017 - moi12.SL2017 - moi17.SL2017 - moi26.SL2017;
            moi27.TT2017 = moi2.TT2017 - moi3.TT2017 - moi6.TT2017 - moi7.TT2017 - moi12.TT2017 - moi17.TT2017 - moi26.TT2017; ;
            moi27.SL2018 = moi2.SL2018 - moi3.SL2018 - moi6.SL2018 - moi7.SL2018 - moi12.SL2018 - moi17.SL2018 - moi26.SL2018; ;
            moi27.TT2018 = moi2.TT2018 - moi3.TT2018 - moi6.TT2018 - moi7.TT2018 - moi12.TT2018 - moi17.TT2018 - moi26.TT2018; ;
            _ds.Add(moi27);

            bc1 moi28 = new bc1();
            moi28.STT = "2";
            moi28.NoiDung = "Thu viện phí trực tiếp";
            moi28.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Sum(p => p.SoLuong);
            moi28.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Sum(p => p.ThanhTien)/1000000;
            moi28.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Sum(p => p.SoLuong);
            moi28.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi28);

            bc1 moi29 = new bc1();
            moi29.STT = "a";
            moi29.NoiDung = "Số khám bệnh";
            moi29.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.SoLuong);
            moi29.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.ThanhTien)/1000000;
            moi29.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.SoLuong);
            moi29.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenTN == "Tiền công khám").Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi29);

            bc1 moi30 = new bc1();
            moi30.STT = "b";
            moi30.NoiDung = "Ngày giường điều trị nội trú";
            moi30.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.SoLuong);
            moi30.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.ThanhTien)/1000000;
            moi30.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.SoLuong);
            moi30.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == "Ngày Giường").Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi30);

            bc1 moi31 = new bc1();
            moi31.STT = "c";
            moi31.NoiDung = "Tổng số phẫu thuật, trong đó:";
            moi31.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi31.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien)/1000000;
            moi31.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi31.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi31);

            bc1 moi32 = new bc1();
            moi32.STT = "";
            moi32.NoiDung = "- Phẫu thuật đặc biệt";
            moi32.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi32.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            moi32.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi32.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi32);

            bc1 moi33 = new bc1();
            moi33.STT = "";
            moi33.NoiDung = "- Phẫu thuật loại 1";
            moi33.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi33.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            moi33.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi33.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi33);

            bc1 moi34 = new bc1();
            moi34.STT = "";
            moi34.NoiDung = "- Phẫu thuật loại 2";
            moi34.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi34.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            moi34.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi34.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi34);

            bc1 moi35 = new bc1();
            moi35.STT = "";
            moi35.NoiDung = "- Phẫu thuật loại 3";
            moi35.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi35.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            moi35.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi35.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.PhauThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi35);

            bc1 moi36 = new bc1();
            moi36.STT = "d";
            moi36.NoiDung = "Tổng số thủ thuật, trong đó:";
            moi36.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi36.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien)/1000000;
            moi36.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.SoLuong);
            moi36.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && (p.Loai == 0 || p.Loai == 1 || p.Loai == 2 || p.Loai == 3)).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi36);

            bc1 moi37 = new bc1();
            moi37.STT = "";
            moi37.NoiDung = "- Thủ thuật đặc biệt";
            moi37.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi37.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            moi37.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.SoLuong);
            moi37.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 0).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi37);

            bc1 moi38 = new bc1();
            moi38.STT = "";
            moi38.NoiDung = "- Thủ thuật loại 1";
            moi38.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi38.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            moi38.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.SoLuong);
            moi38.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 1).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi38);

            bc1 moi39 = new bc1();
            moi39.STT = "";
            moi39.NoiDung = "- Thủ thuật loại 2";
            moi39.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi39.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            moi39.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.SoLuong);
            moi39.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 2).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi39);

            bc1 moi40 = new bc1();
            moi40.STT = "";
            moi40.NoiDung = "- Thủ thuật loại 3";
            moi40.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi40.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            moi40.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.SoLuong);
            moi40.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.ThuThuat && p.Loai == 3).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi40);

            bc1 moi41 = new bc1();
            moi41.STT = "e";
            moi41.NoiDung = "Các xét nghiệm, cận lâm sàng";
            moi41.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi41.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            moi41.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi41.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi41);

            bc1 moi42 = new bc1();
            moi42.STT = "";
            moi42.NoiDung = "- Tổng số xét nghiệm huyết học các loại";
            moi42.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.SoLuong);
            moi42.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.ThanhTien)/1000000;
            moi42.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.SoLuong);
            moi42.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHuyetHoc).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi42);

            bc1 moi43 = new bc1();
            moi43.STT = "";
            moi43.NoiDung = "- Tổng số xét nghiệm hóa sinh các loại";
            moi43.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.SoLuong);
            moi43.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.ThanhTien)/1000000;
            moi43.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.SoLuong);
            moi43.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMau || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhMienDich || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNHoaSinhNTTo).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi43);

            bc1 moi44 = new bc1();
            moi44.STT = "";
            moi44.NoiDung = "- Tổng số xét nghiệm vi sinh các loại";
            moi44.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.SoLuong);
            moi44.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.ThanhTien)/1000000;
            moi44.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.SoLuong);
            moi44.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNViSinh).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi44);

            bc1 moi45 = new bc1();
            moi45.STT = "";
            moi45.NoiDung = "- Tổng số xét nghiệm giải phẫu các loại";
            moi45.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.SoLuong);
            moi45.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.ThanhTien)/1000000;
            moi45.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.SoLuong);
            moi45.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.XNMoBenhHoc).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi45);

            bc1 moi46 = new bc1();
            moi46.STT = "";
            moi46.NoiDung = "- Tổng số lần siêu âm";
            moi46.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.SoLuong);
            moi46.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.ThanhTien)/1000000;
            moi46.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.SoLuong);
            moi46.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm || p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.SieuAm_Doppler).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi46);

            bc1 moi47 = new bc1();
            moi47.STT = "";
            moi47.NoiDung = "- Tổng số lần chụp XQ các loại";
            moi47.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.SoLuong);
            moi47.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.ThanhTien)/1000000;
            moi47.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.SoLuong);
            moi47.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_Quang).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi47);

            bc1 moi48 = new bc1();
            moi48.STT = "";
            moi48.NoiDung = "- Tổng số lần chụp CT Scanner các loại";
            moi48.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.SoLuong);
            moi48.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.ThanhTien)/1000000;
            moi48.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.SoLuong);
            moi48.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangCT).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi48);

            bc1 moi49 = new bc1();
            moi49.STT = "";
            moi49.NoiDung = "- Tổng số lần chụp MRI các loại";
            moi49.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi49.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            moi49.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.SoLuong);
            moi49.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.TenRG == DungChung.Bien.st_TieuNhomRG_ChuyenKhoa.X_QuangKTS).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi49);

            bc1 moi50 = new bc1();
            moi50.STT = "f";
            moi50.NoiDung = "Thuốc, máu, dịch truyền, VTTH, Vật tư thay thế";
            moi50.SL2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.SoLuong);
            moi50.TT2017 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu1 && p.NgayTT <= ngayden1).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.ThanhTien)/1000000;
            moi50.SL2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.SoLuong);
            moi50.TT2018 = ds2.Where(p => p.DTuong == "Dịch vụ").Where(p => p.NgayTT >= ngaytu2 && p.NgayTT <= ngayden2).Where(p => p.IDNhom == 3 || p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7 || p.IDNhom == 10).Sum(p => p.ThanhTien)/1000000;
            _ds.Add(moi50);

            bc1 moi51 = new bc1();
            moi51.STT = "g";
            moi51.NoiDung = "Khác";
            moi51.SL2017 = moi28.SL2017 - moi29.SL2017 - moi30.SL2017 - moi31.SL2017 - moi36.SL2017 - moi41.SL2017 - moi50.SL2017;
            moi51.TT2017 = moi28.TT2017 - moi29.TT2017 - moi30.TT2017 - moi31.TT2017 - moi36.TT2017 - moi41.TT2017 - moi50.TT2017; ;
            moi51.SL2018 = moi28.SL2018 - moi29.SL2018 - moi30.SL2018 - moi31.SL2018 - moi36.SL2018 - moi41.SL2018 - moi50.SL2018; ;
            moi51.TT2018 = moi28.TT2018 - moi29.TT2018 - moi30.TT2018 - moi31.TT2018 - moi36.TT2018 - moi41.TT2018 - moi50.TT2018; ;
            _ds.Add(moi51);

            BaoCao.rep_BCTacDongDieuChinhGiaTT37 rep = new BaoCao.rep_BCTacDongDieuChinhGiaTT37();
            rep.TieuDe1.Text = "Năm " + Nam1.Text + " (về số thu BYT lấy theo số đơn vị đề nghị Quyết toán với cơ quan BHXH)";
            int x = 0;
            if(ngaytu2.Year == ngayden2.Year)
                x = ngayden2.Month - ngaytu2.Month + 1;
            else
                x = 13 - ngaytu2.Month + ngayden2.Month;
            rep.TieuDe2.Text = "Ước " + x + " tháng năm " + ngaytu2.Year;
            rep.DataSource = _ds.ToList();
            rep.BindingData();
            rep.CreateDocument();
            frmIn frm = new frmIn();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void frm_BCDCGiaTT37_Load(object sender, EventArgs e)
        {
            Nam1.Text = Convert.ToString(DateTime.Now.Year - 1);
            NgayTu2.DateTime = Convert.ToDateTime("01/01/2018");
            NgayDen2.DateTime = Convert.ToDateTime("30/06/2018");
        } 
    }
}