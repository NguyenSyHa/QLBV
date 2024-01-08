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
    public partial class frm_ChiPhiChiDinhBenhNhan : DevExpress.XtraEditors.XtraForm
    {
        public frm_ChiPhiChiDinhBenhNhan()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private int _maBN;
        public frm_ChiPhiChiDinhBenhNhan(int MaBN)
        {
            InitializeComponent();
            _maBN = MaBN;
        }
        private class ChiTietChiDinhBN
        {
            public string TenDV { get; set; }
            public string DonVi { get; set; }
            public double TLTT { get; set; }
            public double Mien { get; set; }
            public double SoLuong { get; set; }
            public double DonGia { get; set; }
            public double ThanhTien { get; set; }
            public string KhoaPhong { get; set; }
            public string TrongBH { get; set; }
        }
        private void frm_ChiPhiChiDinhBenhNhan_Load(object sender, EventArgs e)
        {
            lblTenBN.Text = data.BenhNhans.Where(p => p.MaBNhan == _maBN).First().TenBNhan.ToUpper();
            if (chitiet().Count > 0)
            {
                grcChiTietChiDinh.DataSource = chitiet().OrderBy(p => p.TrongBH);
                txtTT.Text = chitiet().Where(p => p.TrongBH != "Không thanh toán").Sum(p => p.ThanhTien).ToString("#,##");            
            }
         
            gridView1.ExpandAllGroups();
        }
        List<ChiTietChiDinhBN> listbn = new List<ChiTietChiDinhBN>();
        private List<ChiTietChiDinhBN> chitiet()
        {
            listbn.Clear();
            var donthuoc = (from dt in data.DThuocs.Where(p => p.MaBNhan == _maBN)
                            join dtct in data.DThuoccts.Where(p => p.IDCD == null && p.IDCLS == null) on dt.IDDon equals dtct.IDDon
                            join dv in data.DichVus on dtct.MaDV equals dv.MaDV
                            join kp in data.KPhongs on dt.MaKP equals kp.MaKP
                            group new { dt, dtct, dv } by new { dt.MaBNhan, dtct.DonGia, dtct.TyLeTT, dtct.Mien, dtct.DonVi, dv.TenDV, kp.TenKP, dtct.TrongBH } into kq
                            select new
                            {
                                TenDV = kq.Key.TenDV,
                                DonVi = kq.Key.DonVi,
                                TLTT = kq.Key.TyLeTT,
                                Mien = kq.Key.Mien,
                                SoLuong = kq.Sum(p => p.dtct.SoLuong),
                                DonGia = kq.Sum(p => p.dtct.DonGia),
                                ThanhTien = kq.Sum(p => p.dtct.ThanhTien),
                                KhoaPhong = kq.Key.TenKP,
                                TrongDMBH = kq.Key.TrongBH,
                            }).ToList();

            var chidinh = (from cls in data.CLS.Where(p => p.MaBNhan == _maBN)
                           join cd in data.ChiDinhs on cls.IdCLS equals cd.IdCLS
                           join dv in data.DichVus on cd.MaDV equals dv.MaDV
                           join kp in data.KPhongs on cls.MaKP equals kp.MaKP
                           select new
                         {
                             TenDV = dv.TenDV,
                             DonVi = dv.DonVi,
                             TLTT = 100,
                             Mien = dv.Mien,
                             SoLuong = 1,
                             DonGia = dv.DonGiaTT39,
                             ThanhTien = dv.DonGiaTT39,
                             KhoaPhong = kp.TenKP,
                             TrongDMBH = cd.TrongBH,
                         }).ToList();

            if (donthuoc.Count() > 0)
            {
                foreach (var item in donthuoc)
                {
                    ChiTietChiDinhBN rep = new ChiTietChiDinhBN();
                    rep.TenDV = item.TenDV;
                    rep.DonVi = item.DonVi;
                    rep.TLTT = item.TLTT;
                    rep.Mien = item.Mien;
                    rep.SoLuong = item.SoLuong;
                    rep.DonGia = item.DonGia;
                    rep.ThanhTien = item.ThanhTien;
                    rep.KhoaPhong = item.KhoaPhong;
                    rep.TrongBH = TrongBH(item.TrongDMBH);
                    listbn.Add(rep);
                }
            }
            if (chidinh.Count() > 0)
            {
                foreach (var item in chidinh)
                {
                    ChiTietChiDinhBN rep = new ChiTietChiDinhBN();
                    rep.TenDV = item.TenDV;
                    rep.DonVi = item.DonVi;
                    rep.TLTT = item.TLTT;
                    rep.Mien = item.Mien;
                    rep.SoLuong = item.SoLuong;
                    rep.DonGia = item.DonGia;
                    rep.ThanhTien = item.ThanhTien;
                    rep.KhoaPhong = item.KhoaPhong;
                    rep.TrongBH = TrongBH(item.TrongDMBH);
                    listbn.Add(rep);
                }
            }

            return listbn;
        }
        private string TrongBH(int? value)
        {
            string a = "";
            switch (value)
            {
                case 0 :
                     a = "Ngoài danh mục";
                    break;
                case 1:
                     a = "Trong danh mục";
                    break;
                case 2 :
                     a = "Không thanh toán";
                    break;
                case 3:
                     a = "Phụ thu";
                    break;
            }
            return a;
        }
    }
}