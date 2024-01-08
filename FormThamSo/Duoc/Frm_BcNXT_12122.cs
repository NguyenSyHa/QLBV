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
    public partial class Frm_BcNXT_12122 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXT_12122()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private bool KTtaoBcNXT()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn đến ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            //if (lupKho.EditValue == null)
            //{
            //    MessageBox.Show("Bạn chưa chọn xã");
            //    lupKho.Focus();
            //    return false;
            //}
            return true;
        }
        List<NhomDV> _lnhom = new List<NhomDV>();
        private void Frm_BcNXTTheoXa_CM08_Load(object sender, EventArgs e)
        {
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            List<KPhong> q = data.KPhongs.Where(p => p.PLoai == DungChung.Bien.st_PhanLoaiKP.KhoaDuoc).ToList();
            q.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupMaKho.Properties.DataSource = q.ToList();

            List<KPhong> khoa = data.KPhongs.Where(p => p.TrongBV == 1).ToList();
            khoa.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            lupKhoa.Properties.DataSource = khoa.ToList();

            List<NhaCC> lNcc = data.NhaCCs.ToList();
            lNcc.Insert(0, new NhaCC { MaCC = "", TenCC = "Tất cả" });
            lupNCC.Properties.DataSource = lNcc;
            lupNCC.Properties.DisplayMember = "TenCC";
            lupNCC.Properties.ValueMember = "MaCC";

            lupMaKho.EditValue = lupMaKho.Properties.GetKeyValueByDisplayText("Tất cả");
            lupKhoa.EditValue = lupKhoa.Properties.GetKeyValueByDisplayText("Tất cả");
            lupNCC.EditValue = lupNCC.Properties.GetKeyValueByDisplayText("Tất cả");
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBcNXT())
            {

                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                int maKP = 0;
                if (lupMaKho.EditValue != null)
                    maKP = Convert.ToInt32(lupMaKho.EditValue);
                string maCC = "";
                if (lupNCC.EditValue != null)
                    maCC = lupNCC.EditValue.ToString();
                int maBPhan = 0;
                if (lupKhoa.Enabled == true && lupKhoa.EditValue != null)
                    maBPhan = Convert.ToInt32(lupKhoa.EditValue);

                var qNhapd = (from nd in data.NhapDs.Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3).Where(p => p.NgayNhap <= denngay)
                                                    .Where(p => maKP == 0 || p.MaKP == maKP).Where(p => maBPhan == 0 || p.MaKPnx == maBPhan).Where(p => maCC == "" || p.MaCC == maCC)
                              join ndct in data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                              join kp in data.KPhongs on nd.MaKP equals kp.MaKP
                              join dtBN in data.DTBNs on ndct.IDDTBN equals dtBN.IDDTBN into kq
                              from kq1 in kq.DefaultIfEmpty()
                              select new
                              {

                                  nd.IDNhap,
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
                                  kp.TenKP
                              }).ToList();

                var qdv = (from dv in data.DichVus.Where(p => p.PLoai == 1)
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new
                           {
                               dv.MaTam,
                               dv.TenDV,
                               tn.TenTN,
                               dv.MaDV,
                               dv.DonVi
                           }).ToList();

                var q1 = (from dt in qNhapd
                          join dv in qdv on dt.MaDV equals dv.MaDV
                          group new { dt, dv } by new { dv.TenTN, dv.TenDV, dv.DonVi, dt.DonGia, dt.MaDV, dt.TenKP, dv.MaTam } into kq
                          select new
                          {
                              kq.Key.MaTam,
                              kq.Key.TenKP,
                              kq.Key.TenTN,
                              kq.Key.TenDV,
                              DVT = kq.Key.DonVi,
                              kq.Key.DonGia,
                              TonDKSL = kq.Where(p => p.dt.NgayNhap < tungay).Sum(p => p.dt.SoLuongN) - kq.Where(p => p.dt.NgayNhap < tungay).Sum(p => p.dt.SoLuongX),
                              TonDKTT = kq.Where(p => p.dt.NgayNhap < tungay).Sum(p => p.dt.ThanhTienN) - kq.Where(p => p.dt.NgayNhap < tungay).Sum(p => p.dt.ThanhTienX),//- kq.Where(p => p.dt.KieuDon == 9 && p.dt.PLoai == 2).Sum(p => p.dt.ThanhTienX),
                              NhapTKSL = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon != 2).Sum(p => p.dt.SoLuongN), // nhập theo hóa đơn và nhập từ kho khác
                              NhapTKTT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon != 2).Sum(p => p.dt.ThanhTienN), // nhập theo hóa đơn và nhập từ kho khác
                              NhapTraLaiSL = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon == 2).Sum(p => p.dt.SoLuongN), // nhập trả dược
                              NhapTraLaiTT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 1 && p.dt.KieuDon == 2).Sum(p => p.dt.ThanhTienN), // nhập trả dược
                              TongXuatSL = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Sum(p => p.dt.SoLuongX),
                              TongXuatTT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 2).Sum(p => p.dt.ThanhTienX),
                              HuHaoSL = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 3).Sum(p => p.dt.SoLuongX),
                              HuHaoTT = kq.Where(p => p.dt.NgayNhap >= tungay && p.dt.NgayNhap <= denngay).Where(p => p.dt.PLoai == 3).Sum(p => p.dt.ThanhTienX),
                              TonCKSL = kq.Where(p => p.dt.NgayNhap <= denngay).Sum(p => p.dt.SoLuongN) - kq.Where(p => p.dt.NgayNhap <= denngay).Sum(p => p.dt.SoLuongX),
                              TonCKTT = kq.Where(p => p.dt.NgayNhap <= denngay).Sum(p => p.dt.ThanhTienN) - kq.Where(p => p.dt.NgayNhap <= denngay).Sum(p => p.dt.ThanhTienX)
                          }).ToList();//.Where(p => p.TonDKSL != 0 || p.TongXuatSL != 0).ToList();
                BaoCao.Rep_BcNXT_12122 rep = new BaoCao.Rep_BcNXT_12122();
                frmIn frm = new frmIn();
                rep.DataSource = q1.OrderBy(p => p.TenTN).ThenBy(p => p.TenDV);

                rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                rep.BindingData();
                rep.CreateDocument();
                frm.prcIN.PrintingSystem = rep.PrintingSystem;
                frm.ShowDialog();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}