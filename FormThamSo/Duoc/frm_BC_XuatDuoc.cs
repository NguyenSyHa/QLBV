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
    public partial class frm_BC_XuatDuoc : DevExpress.XtraEditors.XtraForm
    {
        public frm_BC_XuatDuoc()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void frm_BC_XuatDuoc_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;

            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == "Khoa dược").ToList();
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKho.Properties.DataSource = lkp;
            lupKho.Properties.DisplayMember = "TenKP";
            lupKho.Properties.ValueMember = "MaKP";
            lupKho.EditValue = lupKho.Properties.GetKeyValueByDisplayText("Tất cả");

            List<DTBN> ldtbn = data.DTBNs.ToList();
            ldtbn.Insert(0, new DTBN { IDDTBN = 0, DTBN1 = "Tất cả" });
            lupDTBN.Properties.DataSource = ldtbn;
            lupDTBN.Properties.DisplayMember = "DTBN1";
            lupDTBN.Properties.ValueMember = "IDDTBN";
            lupDTBN.EditValue = lupDTBN.Properties.GetKeyValueByDisplayText("Tất cả");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string _tenkho = lupKho.Properties.GetDisplayText(lupKho.EditValue);
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int maKP = 0;
            if (lupKho.EditValue != null)
                maKP = Convert.ToInt32(lupKho.EditValue);

            int dtbn = 0;
            if (lupDTBN.EditValue != null)
                dtbn = Convert.ToInt32(lupDTBN.EditValue);
            //phân loại nhập dược: 1:Nhập, 2: Xuất, 3: Hư hao.
            var qNhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 2 || p.PLoai == 3).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join dtBN in data.DTBNs on ndct.IDDTBN equals dtBN.IDDTBN into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new
                          {
                              nd.IDNhap,
                              nd.MaKP,
                              ndct.IDDTBN,
                              kq1.DTBN1,
                              nd.Mien,
                              nd.PLoai,
                              nd.KieuDon,
                              nd.XuatTD,
                              nd.NgayNhap,
                              ndct.MaDV,
                              ndct.DonGia,
                              ndct.DonVi,
                              ndct.SoLuongX,
                              ndct.SoLuongN,
                              ndct.ThanhTienN,
                              ndct.ThanhTienX,
                          }).Where(p => maKP == 0 || p.MaKP == maKP).Where(p => dtbn == 0 || p.IDDTBN == dtbn)
                            .Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).ToList();

            var qdv = (from a in qNhapd
                       join dv in data.DichVus.Where(p => p.PLoai == 1) on a.MaDV equals dv.MaDV//thuốc
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           a.IDNhap,
                           a.MaKP,
                           a.IDDTBN,
                           a.DTBN1,
                           a.Mien,
                           a.PLoai,
                           a.KieuDon,
                           a.XuatTD,
                           a.NgayNhap,
                           a.DonGia,
                           a.DonVi,
                           a.SoLuongX,
                           a.SoLuongN,
                           a.ThanhTienN,
                           a.ThanhTienX,
                           dv.TenDV,
                           tn.TenTN,
                           dv.MaDV
                       }).ToList();

            //Kiểu đơn: 4: xuất ngoại trú cho BN không có thể BHYT, kiểu đơn: 0: xuất ngoại trú cho BN có thẻ BHYT
            var q1 = (from n in qdv
                      group n by new { n.TenTN, n.TenDV, n.DonVi, n.DonGia, n.MaDV } into kq
                      select new
                      {
                          kq.Key.TenTN,
                          kq.Key.TenDV,
                          kq.Key.DonVi,
                          kq.Key.DonGia,
                          XuatNT_BHYT_SL = kq.Where(p => p.DTBN1 == "BHYT")
                                             .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1).Sum(p => p.SoLuongX),
                          XuatNT_BHYT_TT = kq.Where(p => p.DTBN1 == "BHYT")
                                             .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1).Sum(p => p.ThanhTienX),
                          XuatNT_DV_SL = kq.Where(p => p.DTBN1 != "BHYT")
                                           .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1).Sum(p => p.SoLuongX),
                          XuatNT_DV_TT = kq.Where(p => p.DTBN1 != "BHYT")
                                           .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1).Sum(p => p.ThanhTienX),
                          XuatNgoaiTru_BHYT_SL = kq.Where(p => p.DTBN1 == "BHYT")
                                                   .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 0).Sum(p => p.SoLuongX),
                          XuatNgoaiTru_BHYT_TT = kq.Where(p => p.DTBN1 == "BHYT")
                                                   .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 0).Sum(p => p.ThanhTienX),
                          XuatNgoaiTru_DV_SL = kq.Where(p => p.DTBN1 != "BHYT")
                                                 .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 4 || p.KieuDon == 0).Sum(p => p.SoLuongX),
                          XuatNgoaiTru_DV_TT = kq.Where(p => p.DTBN1 != "BHYT")
                                                 .Where(p => p.PLoai == 2).Where(p => p.KieuDon == 4 || p.KieuDon == 0).Sum(p => p.ThanhTienX),
                          XuatNhuong_SL = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 9).Sum(p => p.SoLuongX),
                          XuatNhuong_TT = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 9).Sum(p => p.ThanhTienX),
                          XuatKiemNghiem_SL = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 8).Sum(p => p.SoLuongX),
                          XuatKiemNghiem_TT = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 8).Sum(p => p.ThanhTienX),
                          XuatXa_SL = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 3).Sum(p => p.SoLuongX),
                          XuatXa_TT = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 3).Sum(p => p.ThanhTienX),
                          HongVo_SL = kq.Where(p => p.PLoai == 3).Where(p => p.Mien <= 0).Sum(p => p.SoLuongX),  // hỏng vỡ, hư hao
                          HongVo_TT = kq.Where(p => p.PLoai == 3).Where(p => p.Mien <= 0).Sum(p => p.ThanhTienX),
                      }).Where(p => p.XuatNT_BHYT_SL != 0 || p.XuatNT_DV_SL != 0 || p.XuatNgoaiTru_BHYT_SL != 0 || p.XuatNgoaiTru_DV_SL != 0 ||
                               p.XuatNhuong_SL != 0 || p.XuatKiemNghiem_SL != 0 || p.XuatXa_SL != 0 || p.HongVo_SL != 0).ToList();

            BaoCao.Rep_BC_XuatDuoc rep = new BaoCao.Rep_BC_XuatDuoc();
            frmIn frm = new frmIn();
            rep.DataSource = q1.OrderBy(p => p.TenDV);
            rep.lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            rep.lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            rep.lblTieuDe.Text = "BÁO CÁO XUẤT DƯỢC - " + _tenkho.ToUpper();
            rep.lblThoiGian.Text = "(Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy") + ")";
            rep.BindingData();
            rep.CreateDocument();
            frm.prcIN.PrintingSystem = rep.PrintingSystem;
            frm.ShowDialog();
        }
    }
}