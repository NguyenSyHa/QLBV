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
    public partial class frm_BC_BBKiemKe_30005 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_BBKiemKe_30005()
        {
            InitializeComponent();
        }

        private class KPhong
        {
            private string TenKP;
            private int MaKP;
            private bool Chon;
            public string tenkp
            { set { TenKP = value; } get { return TenKP; } }
            public int makp
            { set { MaKP = value; } get { return MaKP; } }
            public bool chon
            { set { Chon = value; } get { return Chon; } }
        }

        List<KPhong> _Kphong = new List<KPhong>();

        private void frm_BC_BBKiemKe_30005_Load(object sender, EventArgs e)
        {
            date_TuNgay.DateTime = DateTime.Now;
            date_DenNgay.DateTime = DateTime.Now;
            timeTuGio.Properties.Mask.EditMask = "HH:mm";
            timeDenGio.Properties.Mask.EditMask = "HH:mm";

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var kphong = (from kp in data.KPhongs.Where(p => p.PLoai == "Khoa dược")
                          select new { kp.TenKP, kp.MaKP }).ToList();
            if (kphong.Count > 0)
            {
                KPhong themmoi1 = new KPhong();
                themmoi1.tenkp = "Chọn tất cả";
                themmoi1.makp = 0;
                themmoi1.chon = true;
                _Kphong.Add(themmoi1);
                foreach (var a in kphong)
                {
                    KPhong themmoi = new KPhong();
                    themmoi.tenkp = a.TenKP;
                    themmoi.makp = a.MaKP;
                    themmoi.chon = true;
                    _Kphong.Add(themmoi);
                }
                grcKhoaphong.DataSource = _Kphong.ToList();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(date_TuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(date_DenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _lKhoaP = new List<KPhong>();
            _lKhoaP = _Kphong.Where(p => p.makp > 0).Where(p => p.chon == true).ToList();
            string tenkho = string.Empty;
            foreach (var item in _lKhoaP)
            {
                string _kho = data.KPhongs.Where(p => p.MaKP == item.makp).FirstOrDefault().TenKP;
                tenkho += _kho + ", ";
            }
            var nd1 = (from nd in data.NhapDs.Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3).Where(p => p.NgayNhap <= denngay)
                       join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                       select new {
                           nd.IDNhap,
                           ndct.IDDTBN,
                           nd.Mien,
                           nd.PLoai,
                           nd.KieuDon,
                           nd.XuatTD,
                           nd.NgayNhap,
                           ndct.MaDV,
                           ndct.DonGia,
                           ndct.SoLuongX,
                           ndct.SoLuongN,
                           ndct.ThanhTienN,
                           ndct.ThanhTienX,
                           nd.MaKP,
                       }).ToList();
            var qNhapd = (from kp in _lKhoaP
                          join nd in nd1 on kp.makp equals nd.MaKP
                          join dtBN in data.DTBNs on nd.IDDTBN equals dtBN.IDDTBN into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new
                          {
                              nd.IDNhap,
                              nd.IDDTBN,
                              DTBN = kq1 == null ? "" : kq1.DTBN1,
                              nd.Mien,
                              nd.PLoai,
                              nd.KieuDon,
                              nd.XuatTD,
                              nd.NgayNhap,
                              nd.MaDV,
                              nd.DonGia,
                              nd.SoLuongX,
                              nd.SoLuongN,
                              nd.ThanhTienN,
                              nd.ThanhTienX,
                          }).ToList();

            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           dv.TenDV,
                           dv.DonVi,
                           tn.TenTN,
                           dv.MaDV,
                           dv.MaCC,
                           dv.IDNhom,
                           tn.STT
                       }).ToList();

            var q1 = (from dt in qNhapd
                      join dv in qdv on dt.MaDV equals dv.MaDV
                      group new { dt, dv } by new { dv.TenTN, dv.TenDV, dv.DonVi, dt.DonGia, dt.MaDV } into kq
                      select new
                      {
                          kq.Key.TenTN,
                          kq.Key.TenDV,
                          kq.Key.DonVi,
                          kq.Key.DonGia,
                          TonDK = kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN) -
                                  kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PLoai == 2 || p.dt.PLoai == 3).Sum(p => p.dt.SoLuongX),
                          NhapKho = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon != 2).Sum(p => p.dt.SoLuongN), // nhập theo hóa đơn và nhập từ kho khác
                          NhapTraLai = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon == 2).Sum(p => p.dt.SoLuongN), // nhập trả dược
                          TongNhap = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN),
                          HongVo = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 3).Where(p => p.dt.Mien <= 0).Sum(p => p.dt.SoLuongX),  // hỏng vỡ

                          XuatNhanDan = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2)
                                          .Where(p => p.dt.Mien <= 0).Where(p => p.dt.DTBN != "BHYT" && p.dt.DTBN != "").Where(p => p.dt.KieuDon != 9).Sum(p => p.dt.SoLuongX),//chỉ BN nội trú không có thẻ BHYT

                          XuatKhac = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2)
                                       .Where(p => p.dt.Mien <= 0).Where(p => p.dt.KieuDon == 9).Sum(p => p.dt.SoLuongX),
                          XuatBH = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2)
                                     .Where(p => p.dt.Mien <= 0).Where(p => p.dt.DTBN == "BHYT").Where(p => p.dt.KieuDon != 9).Sum(p => p.dt.SoLuongX),
                          XuatMien = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2)
                                       .Where(p => p.dt.Mien > 0).Sum(p => p.dt.SoLuongX),
                          TongXuat = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Sum(p => p.dt.SoLuongX),
                          TonCuoi = kq.Where(p => p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN) -
                                    kq.Where(p => p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2 || p.dt.PLoai == 3).Sum(p => p.dt.SoLuongX)
                      }).Where(p => p.TonDK != 0 || p.TongNhap != 0 || p.TongXuat != 0).ToList();

            BaoCao.Rep_BC_BBKiemKe_30005 rep = new BaoCao.Rep_BC_BBKiemKe_30005();
            frmIn frm = new frmIn();
            rep.DataSource = q1.OrderBy(p => p.TenDV);
            rep.lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.lblKho.Text = "Kho: " + tenkho;
            rep.lblThoiGian.Text = "Hôm nay vào hồi " + Convert.ToDateTime(timeTuGio.EditValue).ToString("HH:mm") + " giờ đến " + Convert.ToDateTime(timeDenGio.EditValue).ToString("HH:mm") + " giờ ngày " + DateTime.Now.ToString("dd/MM/yyyy") +
                                    " tại khoa Dược đã tiến hành kiểm kê thuốc, VTYT từ " + tungay.ToString("dd/MM/yyyy") + " đến " + denngay.ToString("dd/MM/yyyy");
            rep.lblTen1.Text = txtTen1.Text;
            rep.lblTen2.Text = txtTen2.Text;
            rep.lblTen3.Text = txtTen3.Text;
            rep.lblTen4.Text = txtTen4.Text;
            rep.lblTen5.Text = txtTen5.Text;
            rep.lblChucVu1.Text = txtChucVu1.Text;
            rep.lblChucVu2.Text = txtChucVu2.Text;
            rep.lblChucVu3.Text = txtChucVu3.Text;
            rep.lblChucVu4.Text = txtChucVu4.Text;
            rep.lblChucVu5.Text = txtChucVu5.Text;
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }

        private void grvKhoaphong_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "Chọn")
            {
                if (grvKhoaphong.GetFocusedRowCellValue("tenkp") != null)
                {
                    string Ten = grvKhoaphong.GetFocusedRowCellValue("tenkp").ToString();

                    if (Ten == "Chọn tất cả")
                    {
                        if (_Kphong.First().chon == true)
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = false;
                            }
                        }
                        else
                        {
                            foreach (var a in _Kphong)
                            {
                                a.chon = true;
                            }
                        }
                        grcKhoaphong.DataSource = "";
                        grcKhoaphong.DataSource = _Kphong.ToList();
                    }
                }
            }
        }
    }
}