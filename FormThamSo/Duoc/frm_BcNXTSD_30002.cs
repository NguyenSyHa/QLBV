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
    public partial class frm_BcNXTSD_30002 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Báo cáo nhập xuất tồn (xuất trong kỳ = xuất của ko + sử dụng của phòng khám khu vực + xã)
        /// </summary>
        public frm_BcNXTSD_30002()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_NXT_30002_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;

            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

            List<KPhong> lKho = new List<KPhong>();
            List<KPhong> lKhoa = new List<KPhong>();
            List<NhaCC> lNcc = new List<NhaCC>();

            lKho = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            lKhoa = data.KPhongs.Where(p=>p.PLoai != DungChung.Bien.st_PhanLoaiKP.Admin & p.PLoai != DungChung.Bien.st_PhanLoaiKP.HanhChinh && p.PLoai != DungChung.Bien.st_PhanLoaiKP.KeToan).ToList();
            lNcc = data.NhaCCs.OrderBy(p=>p.TenCC).ToList();
            lKho.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lKhoa.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lNcc.Insert(0, new NhaCC { MaCC = "", TenCC = "Tất cả" });

            lupKho.Properties.DisplayMember = "TenKP";
            lupKho.Properties.ValueMember = "MaKP";
            lupKho.Properties.DataSource = lKho;
            lupKho.EditValue = lupKho.Properties.GetKeyValueByDisplayText("Tất cả");
            lupNcc.Properties.DisplayMember = "TenCC";
            lupNcc.Properties.ValueMember = "MaCC";
            lupNcc.Properties.DataSource = lNcc;
            lupNcc.EditValue = lupNcc.Properties.GetKeyValueByDisplayText("Tất cả");
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            int maKho = 0;
            int maKhoa = 0;
            string macc = "";
            if (lupKho.EditValue != null)
                maKho = Convert.ToInt32(lupKho.EditValue);
            if(lupNcc.EditValue != null)
                macc = lupNcc.EditValue.ToString();

            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1).Where(p => macc == "" || p.MaCC == macc)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { 
                       dv.TenDV, dv.MaDV, tn.TenTN, n.TenNhom
                       }).ToList();

            var qnd = (from nd in data.NhapDs.Where(p => p.NgayNhap <= denngay).Where(p=> maKho == 0 || p.MaKP == maKho)
                       join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                       select new
                       {
                           nd.NgayNhap,
                           nd.PLoai,
                           nd.KieuDon,
                           nd.XuatTD,
                           ndct.MaDV,
                           ndct.DonVi,
                           ndct.DonGia,
                           SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                           SoLuongX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.SoLuongX : 0,
                           ThanhTienN = nd.PLoai == 1 ? ndct.ThanhTienN : 0,
                           ThanhTienX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.ThanhTienX : 0,
                           ndct.SoLuongSD,
                           ndct.ThanhTienSD,
                       }).ToList();
            var q1 = (from nd in qnd
                      group nd by new { nd.MaDV, nd.DonVi, nd.DonGia } into kq
                      select new { 
                      kq.Key.MaDV,
                      kq.Key.DonGia,
                      kq.Key.DonVi,
                      TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 2 && p.KieuDon != 3).Sum(p => p.SoLuongX)  - kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 5).Sum(p => p.SoLuongSD),
                      TonDKTT = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 2 && p.KieuDon != 3).Sum(p => p.ThanhTienX)  - kq.Where(p => p.NgayNhap < tungay).Where(p => p.PLoai == 5).Sum(p => p.ThanhTienSD),

                      NhapTKSL = kq.Where(p=>p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p=>p.SoLuongN),
                      NhapTKTT = kq.Where(p=>p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p=>p.ThanhTienN),

                      XuatNoiTruSL = kq.Where(p=>p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.PLoai == 2 && p.KieuDon == 1).Sum(p=>p.SoLuongX),
                      XuatNoiTruTT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 2 && p.KieuDon == 1).Sum(p => p.ThanhTienX),

                      XuatNgTruSL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.SoLuongX),
                      XuatNgTruTT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 2 && p.KieuDon == 0).Sum(p => p.ThanhTienX),

                      XuatXaSL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 5).Sum(p => p.SoLuongSD),// sử dụng của xã phường, phòng khám khu vực
                      XuatXaTT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 5).Sum(p => p.ThanhTienSD),// sử dụng của xã phường, phòng khám khu vực

                      XuatTKSL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.SoLuongX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.PLoai == 2 && p.KieuDon == 3).Sum(p => p.SoLuongX) +kq.Where(p=>p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.PLoai == 5).Sum(p=>p.SoLuongSD),
                      XuatTKTT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 2 && p.KieuDon == 3).Sum(p => p.ThanhTienX) + kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p => p.PLoai == 5).Sum(p => p.ThanhTienSD),

                      TonCKSL = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 2 && p.KieuDon != 3).Sum(p => p.SoLuongX)  - kq.Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 5).Sum(p => p.SoLuongSD),
                      TonCKTT = kq.Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 2 && p.KieuDon != 3).Sum(p => p.ThanhTienX) - kq.Where(p => p.NgayNhap <= denngay).Where(p => p.PLoai == 5).Sum(p => p.ThanhTienSD),

                      }).Where(P=>P.TonDKSL != 0 || P.NhapTKSL != 0 || P.XuatTKSL != 0).ToList();
            var q2 = (from nd in q1
                      join dv in qdv on nd.MaDV equals dv.MaDV
                      select new
                          {
                              dv.TenDV,
                              dv.TenNhom,
                              dv.TenTN,
                              nd.DonGia,
                              nd.DonVi,
                              nd.TonDKSL,
                              nd.TonDKTT,
                              nd.NhapTKSL,
                              nd.NhapTKTT,
                              nd.XuatNgTruSL,
                              nd.XuatNgTruTT,
                              nd.XuatNoiTruSL,
                              nd.XuatNoiTruTT,
                              nd.XuatXaSL,
                              nd.XuatXaTT,
                              nd.XuatTKSL,
                              nd.XuatTKTT,
                              nd.TonCKSL,
                              nd.TonCKTT
                          }).ToList();

            frmIn frm = new frmIn();
            BaoCao.RepBcNXTSD_30002 rep = new BaoCao.RepBcNXTSD_30002();
            rep.TuNgay.Value = tungay.ToString("dd/MM/yyyy");
            rep.DenNgay.Value = denngay.ToString("dd/MM/yyyy");
            rep.Kho.Value = lupKho.Text == "Tất cả" ? "" : lupKho.Text;
            rep.NhaCC.Value = lupNcc.Text == "Tất cả" ? "" : lupNcc.Text;
            rep.BindingData();
            rep.DataSource = q2.OrderBy(p => p.TenDV).ToList();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}