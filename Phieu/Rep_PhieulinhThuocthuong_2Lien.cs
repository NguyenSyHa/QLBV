using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieulinhThuocthuong_2Lien : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieulinhThuocthuong_2Lien()
        {
            InitializeComponent();
        }

        
        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _SP = Convert.ToInt32(SoPL.Value.ToString());
            var a = (from DT in _data.DThuocs
                     join DTct in _data.DThuoccts.Where(p => p.SoPL == _SP) on DT.IDDon equals DTct.IDDon
                     join dv in _data.DichVus on DTct.MaDV equals dv.MaDV
                     group new { DTct, dv } by new { DTct.MaDV, dv.TenDV, DTct.DonGia, dv.DonVi } into kq
                     select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.MaDV, kq.Key.TenDV, SL = kq.Sum(p => p.DTct.SoLuong), ThanhTien = kq.Sum(p => p.DTct.ThanhTien) }).ToList();
            // var q =(from a in _data.VienPhicts select new {SoLuongT= a.SoLuong,ThanhTienT= a.ThanhTien }).ToList();
            Rep_SubPLLien1 rep = (Rep_SubPLLien1)xrSubreport1.ReportSource;
            var ngay = _data.DThuoccts.Where(p => p.SoPL == _SP).Select(p => p.NgayNhap).OrderBy(p => p.Value).ToList();
            string ngay1 = "";
            string ngay2 = "";
            if (ngay.Count > 0)
            {
                ngay1 = ngay.First().ToString().Substring(0, 10);
                ngay2 = ngay.Last().ToString().Substring(0, 10);
            }
            rep.Ngaythang.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
            var kd = (from dt in _data.DThuocs
                      join dtct in _data.DThuoccts.Where(p => p.SoPL == _SP) on dt.IDDon equals dtct.IDDon
                      select new { dt.KieuDon }).ToList();
                if(kd.First().KieuDon.Value==0)
                {
                    rep.LoaiPL.Value = "Loại phiếu: Thường xuyên";
                }
                if (kd.First().KieuDon.Value == 1)
                { rep.LoaiPL.Value = "Loại phiếu: Bổ sung"; }
                if (kd.First().KieuDon.Value == 2)
                { rep.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }
                if (kd.First().KieuDon.Value == 3)
                { rep.LoaiPL.Value = "Loại phiếu: Lĩnh về cho khoa"; }
                if (kd.First().KieuDon.Value == 4)
            {rep.LoaiPL.Value="Loại phiếu: Trả thuốc cho khoa";}
            var kho = (from dt in _data.DThuoccts.Where(p => p.SoPL == _SP)
                       join kp in _data.KPhongs on dt.MaKXuat equals kp.MaKP
                       select new { kp.TenKP }).ToList();
            if (kho.Count > 0 && kho.First().TenKP != null && kho.First().TenKP.ToString() != "")
            { rep.KhoLinh.Value = "Kho lĩnh: " + kho.First().TenKP; }
            var khoa = (from dt in _data.DThuoccts.Where(p => p.SoPL == _SP)
                       join kp in _data.KPhongs on dt.MaKP equals kp.MaKP
                       select new { kp.TenKP }).ToList();
            if (khoa.Count > 0 && khoa.First().TenKP != null && khoa.First().TenKP.ToString() != "")
            { rep.Khoa.Value = khoa.First().TenKP.ToUpper(); }
            rep.SoPL.Value = _SP;
            rep.Khoan.Value = "Tổng số: " + a.Count + " khoản";
            rep.Tien.Value = "Tổng tiền: " + a.Sum(p => p.ThanhTien);
            //rep.Ngay.Value = this.Ngay.Value;
            rep.DataSource = a.ToList();
            rep.BindingData();
        }

        private void xrSubreport2_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int _SP = Convert.ToInt32(SoPL.Value.ToString());
            var a = (from DT in _data.DThuocs
                     join DTct in _data.DThuoccts.Where(p => p.SoPL == _SP) on DT.IDDon equals DTct.IDDon
                     join dv in _data.DichVus on DTct.MaDV equals dv.MaDV
                     group new { DTct, dv } by new { DTct.MaDV, dv.TenDV, DTct.DonGia, dv.DonVi } into kq
                     select new { kq.Key.DonGia, kq.Key.DonVi, kq.Key.MaDV, kq.Key.TenDV, SL = kq.Sum(p => p.DTct.SoLuong), ThanhTien = kq.Sum(p => p.DTct.ThanhTien) }).ToList();
            // var q =(from a in _data.VienPhicts select new {SoLuongT= a.SoLuong,ThanhTienT= a.ThanhTien }).ToList();
            Rep_SubPLLien2 rep = (Rep_SubPLLien2)xrSubreport2.ReportSource;
            var ngay = (from dt in _data.DThuocs
                        join dtct in _data.DThuoccts.Where(p => p.SoPL == _SP) on dt.IDDon equals dtct.IDDon
                        select new { dt.NgayKe }).OrderBy(p => p.NgayKe).ToList();
            string ngay1 = "";
            string ngay2 = "";
            if (ngay.Count > 0)
            {
                ngay1 = ngay.First().ToString().Substring(0, 10);
                ngay2 = ngay.Last().ToString().Substring(0, 10);
            }
            rep.Ngaythang.Value = "Từ ngày: " + ngay1 + " đến ngày: " + ngay2;
            var kd =(from dt in _data.DThuocs
                    join dtct in _data.DThuoccts.Where(p => p.SoPL == _SP) on dt.IDDon equals dtct.IDDon
                    select new { dt.KieuDon}).ToList();
            if (kd.First().KieuDon.Value == 0)
            {
                rep.LoaiPL.Value = "Loại phiếu: Thường xuyên";
            }
            if (kd.First().KieuDon.Value == 1)
            { rep.LoaiPL.Value = "Loại phiếu: Bổ sung"; }
            if (kd.First().KieuDon.Value == 2)
            { rep.LoaiPL.Value = "Loại phiếu: Trả thuốc"; }
            if (kd.First().KieuDon.Value == 3)
            { rep.LoaiPL.Value = "Loại phiếu: Lĩnh về cho khoa"; }
            if (kd.First().KieuDon.Value == 4)
            { rep.LoaiPL.Value = "Loại phiếu: Trả thuốc cho khoa"; }
            var kho = (from dt in _data.DThuocs
                       join dtct in _data.DThuoccts.Where(p => p.SoPL == _SP) on dt.IDDon equals dtct.IDDon
                       join kp in _data.KPhongs on dt.MaKXuat equals kp.MaKP
                       select new { kp.TenKP }).ToList();
            if (kho.Count > 0 && kho.First().TenKP != null && kho.First().TenKP.ToString() != "")
            { rep.KhoLinh.Value = "Kho lĩnh: " + kho.First().TenKP; }
            var khoa = (from dt in _data.DThuocs
                        join dtct in _data.DThuoccts.Where(p => p.SoPL == _SP) on dt.IDDon equals dtct.IDDon
                        join kp in _data.KPhongs on dt.MaKP equals kp.MaKP
                        select new { kp.TenKP }).ToList();
            if (khoa.Count > 0 && khoa.First().TenKP != null && khoa.First().TenKP.ToString() != "")
            { rep.Khoa.Value =  khoa.First().TenKP.ToUpper(); }
            rep.SoPL1.Value = _SP;
            rep.Khoan.Value = "Tổng số: " + a.Count + " khoản";
            rep.Tien.Value = "Tổng tiền: " + a.Sum(p => p.ThanhTien);
            //rep.Ngay.Value = this.Ngay.Value;
            rep.DataSource = a;
            rep.BindingData();
        }

    }
}
