using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLBV.FormThamSo.Duoc
{
    public class BCKhongThamSo
    {
        private static QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public static void In_Id534()
        {
            var dataSource = Id534_GetData();
            frmIn frm = new frmIn();
            BaoCao.Rep_BC_DanhMucThuoc_Mau16_20001 rep = new BaoCao.Rep_BC_DanhMucThuoc_Mau16_20001();
            rep.NgayThangNam.Value = "...., ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.lblTenCQCQ.Text = "CƠ SỞ Y TẾ " + (!string.IsNullOrEmpty(DungChung.Bien.TenCQ) ? DungChung.Bien.TenCQ.ToUpper() : "");
            rep.DataSource = dataSource;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private static object Id534_GetData()
        {
            object rs = null;

            rs = (from tn in data.TieuNhomDVs.Where(p => p.TenTN == "Chế phẩm YHCT" || p.TenTN == "Chế phẩm bệnh viện")
                  join dv in data.DichVus.Where(o => o.IDNhom == 4 && (o.BHTT == 100 || o.BHTT == 50 || o.BHTT == 30))
                             on tn.IdTieuNhom equals dv.IdTieuNhom
                  select new
                  {
                      TenTieuNhom = (
                          tn.TenTN == "Chế phẩm YHCT" ? "A. Chế phẩm YHCT có trong danh mục" : "B. Chế phẩm YHCT do cơ sở y tế tự bào chế"),
                      tn.TenTN,
                      dv.MaQD,
                      dv.TenDV,
                      dv.TenHC,
                      dv.DuongD,
                      dv.HamLuong,
                      dv.NhaSX,
                      dv.NuocSX,
                      dv.SoDK,
                      dv.DonVi,
                      dv.DonGia,
                      dv.DonGia2,
                      dv.GhiChu,
                      TenLoaiHuongBH = (dv.BHTT == 100 ? " 1. Danh mục thuốc được thanh toán 100%" : " 2. Danh mục thuốc được thanh toán 50%, 30%")
                  }).ToList();

            return rs;
        }

        //public class DichVuADO : DichVu
        //{
        //    public DichVuADO()
        //    { }
        //    public DichVuADO(string)
        //    { 
        //        if(data.Te)
        //    }
        //    public string TenTieuNhom { get; set; }
        //}

        public static void In_Id535()
        {
            var dataSource = Id535_GetData();
            frmIn frm = new frmIn();
            BaoCao.Rep_BC_DanhMucThuoc_Mau17_20001 rep = new BaoCao.Rep_BC_DanhMucThuoc_Mau17_20001();
            rep.NgayThangNam.Value = "...., ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            rep.lblTenCQCQ.Text = "CƠ SỞ Y TẾ " + (!string.IsNullOrEmpty(DungChung.Bien.TenCQ) ? DungChung.Bien.TenCQ.ToUpper() : "");
            rep.DataSource = dataSource;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private static object Id535_GetData()
        {
            object rs = null;

            rs = (from tn in data.TieuNhomDVs.Where(p => p.TenTN == "Chế phẩm YHCT" || p.TenTN == "Chế phẩm bệnh viện")
                  join dv in data.DichVus.Where(o => o.IDNhom == 4 && (o.BHTT != null))
                             on tn.IdTieuNhom equals dv.IdTieuNhom
                  //join dd in data.DuongDungs on dv.MaDuongDung equals dd.MaDuongDung
                  select new
                  {
                      tn.TenTN,
                      dv.NguonGoc,
                      dv.TinhTNhap,
                      dv.MaDuongDung,
                      dv.YCSD,
                      dv.TyLeBQ,
                      dv.MaQD,
                      dv.TenDV,
                      dv.TenHC,
                      dv.DuongD,
                      dv.HamLuong,
                      dv.NhaSX,
                      dv.NuocSX,
                      dv.SoDK,
                      dv.DonVi,
                      dv.DonGia,
                      dv.DonGia2,
                      dv.GhiChu
                  }).ToList();

            return rs;
        }
    }
}
