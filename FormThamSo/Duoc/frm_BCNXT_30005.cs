using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace QLBV.FormThamSo
{
    public partial class frm_BCNXT_30005 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCNXT_30005()
        {
            InitializeComponent();
        }

        private void frm_BCNXT_30005_Load(object sender, EventArgs e)
        {
            lupTuNgay.DateTime = DateTime.Now;
            lupDenNgay.DateTime = DateTime.Now;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> lkp = data.KPhongs.Where(p => p.PLoai == "Khoa dược").ToList();
            lkp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKho.Properties.DataSource = lkp;
            lupKho.Properties.DisplayMember = "TenKP";
            lupKho.Properties.ValueMember = "MaKP";

            List<KPhong> lBoPhan = data.KPhongs.Where(p => p.TrongBV == 1).ToList();
            lBoPhan.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupBoPhan.Properties.DataSource = lBoPhan;
            lupBoPhan.Properties.DisplayMember = "TenKP";
            lupBoPhan.Properties.ValueMember = "MaKP";

            List<NhaCC> lNcc = data.NhaCCs.ToList();
            lNcc.Insert(0, new NhaCC { MaCC = "", TenCC = "Tất cả" });
            lupNCC.Properties.DataSource = lNcc;
            lupNCC.Properties.DisplayMember = "TenCC";
            lupNCC.Properties.ValueMember = "MaCC";

            lupKho.EditValue = lupKho.Properties.GetKeyValueByDisplayText("Tất cả");
            lupBoPhan.EditValue = lupBoPhan.Properties.GetKeyValueByDisplayText("Tất cả");
            lupNCC.EditValue = lupNCC.Properties.GetKeyValueByDisplayText("Tất cả");

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            int maKP = 0;
            if (lupKho.EditValue != null)
                maKP = Convert.ToInt32(lupKho.EditValue);
            string maCC = "";
            if (lupNCC.EditValue != null)
                maCC = lupNCC.EditValue.ToString();
            int maBPhan = 0;
            if (lupBoPhan.Enabled == true && lupBoPhan.EditValue != null)
                maBPhan = Convert.ToInt32(lupBoPhan.EditValue);

            var qNhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3).Where(p => p.NgayNhap <= denngay).Where(p => maKP == 0 || p.MaKP == maKP).Where(p => maBPhan == 0 || p.MaKPnx == maBPhan).Where(p => maCC == "" || p.MaCC == maCC)
                          join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                          join dtBN in data.DTBNs on ndct.IDDTBN equals dtBN.IDDTBN into kq
                          from kq1 in kq.DefaultIfEmpty()
                          select new
                          {
                              nd.IDNhap,
                              ndct.IDDTBN,
                              DTBN1 = kq1 == null ? "" : kq1.DTBN1,
                              nd.Mien,
                              nd.PLoai,
                              nd.KieuDon,
                              nd.XuatTD,
                              nd.NgayNhap,
                              ndct.MaDV,
                              ndct.DonGia,
                              ndct.DonVi,
                              SoLuongN = nd.PLoai == 1 ? ndct.SoLuongN : 0,
                              SoLuongX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.SoLuongX : 0,
                              ThanhTienN = nd.PLoai == 1 ? ndct.ThanhTienN : 0,
                              ThanhTienX = (nd.PLoai == 2 || nd.PLoai == 3) ? ndct.ThanhTienX : 0,

                          }).ToList();

            var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       select new
                       {
                           dv.TenDV,
                           tn.TenTN,
                           dv.MaDV
                       }).ToList();


            var q1 = (from dt in qNhapd
                      join dv in qdv on dt.MaDV equals dv.MaDV
                      group new { dt, dv } by new { dv.TenTN, dv.TenDV, dt.DonVi, dt.DonGia, dt.MaDV, dt.Mien } into kq
                      select new
                      {
                          kq.Key.TenTN,
                          kq.Key.TenDV,
                          kq.Key.DonVi,
                          kq.Key.DonGia,
                          TonDK = kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN) - kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PLoai == 2).Sum(p => p.dt.SoLuongX) - kq.Where(p => p.dt.NgayNhap < tungay).Where(p => p.dt.PLoai == 3).Sum(p => p.dt.SoLuongX),
                          NhapKho = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon != 2).Sum(p => p.dt.SoLuongN), // nhập theo hóa đơn và nhập từ kho khác
                          NhapTraLai = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon == 2).Sum(p => p.dt.SoLuongN), // nhập trả dược
                          TongNhap = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN),
                          HongVo = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 3).Where(p => p.dt.Mien <= 0).Sum(p => p.dt.SoLuongX),  // hỏng vỡ
                          XuatNhanDan = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Where(p => p.dt.Mien <= 0).Where(p => p.dt.DTBN1 != "BHYT").Sum(p => p.dt.SoLuongX),
                          XuatNhanDan_TT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Where(p => p.dt.Mien <= 0).Where(p => p.dt.DTBN1 != "BHYT").Sum(p => p.dt.ThanhTienX),
                          XuatBH = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Where(p => p.dt.Mien <= 0).Where(p => p.dt.DTBN1 == "BHYT").Sum(p => p.dt.SoLuongX),
                          XuatBH_TT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Where(p => p.dt.Mien <= 0).Where(p => p.dt.DTBN1 == "BHYT").Sum(p => p.dt.ThanhTienX),
                          XuatMien = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Where(p => p.dt.Mien > 0).Sum(p => p.dt.SoLuongX),
                          XuatMien_TT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Where(p => p.dt.Mien > 0).Sum(p => p.dt.ThanhTienX),
                          TongXuat = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Sum(p => p.dt.SoLuongX),
                          TongXuat_TT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Sum(p => p.dt.ThanhTienX),
                          TonCuoi = kq.Where(p => p.dt.PLoai == 1).Sum(p => p.dt.SoLuongN) - kq.Where(p => p.dt.PLoai == 2).Sum(p => p.dt.SoLuongX) - kq.Where(p => p.dt.PLoai == 3).Sum(p => p.dt.SoLuongX),

                      }).Where(p => p.TonDK != 0 || p.TongNhap != 0 || p.TongXuat != 0).ToList();

            if (rdMau.SelectedIndex == 0)
            {
                BaoCao.Rep_BCNXT_30005 rep = new BaoCao.Rep_BCNXT_30005();
                frmIn frm = new frmIn();
                rep.DataSource = q1;
                rep.lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                rep.lab_Tenkho.Text = "KHOA DƯỢC";
                rep.lab_tungaydenngay.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                rep.Bindingdata();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
            else
            {
                BaoCao.Rep_BCNXT_XuatD_30005 rep = new BaoCao.Rep_BCNXT_XuatD_30005();
                frmIn frm = new frmIn();
                rep.DataSource = q1.Where(p=>p.TongXuat != 0).OrderBy(p=>p.TenDV);
                rep.lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                if (lupKho.EditValue != null && lupKho.Text != "Tất cả")
                    rep.cel_tit.Text = ("Báo cáo xuất thuốc, vật tư y tế " + lupKho.Text).ToUpper();
                rep.lab_Tenkho.Text = "Xuất BP: " + lupBoPhan.Text;
                rep.lab_tungaydenngay.Text = "Từ ngày " + lupTuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lupDenNgay.DateTime.ToString("dd/MM/yyyy");
                rep.Bindingdata();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdMau_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void rdMau_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rdMau.SelectedIndex == 1)
                lupBoPhan.Properties.ReadOnly = false;
            else
            {
                lupBoPhan.Properties.ReadOnly = true;
                lupBoPhan.EditValue = lupBoPhan.Properties.GetKeyValueByDisplayText("Tất cả");
            }
        }
    }
}