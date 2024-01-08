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
    public partial class frm_BCNXT_VAT : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCNXT_VAT()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void frm_BCNXT_VAT_Load(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            deNgayTu.DateTime = DateTime.Now;
            deNgayDen.DateTime = DateTime.Now;
            List<KPhong> dskp = new List<KPhong>();
            dskp = data.KPhongs.Where(p => p.PLoai == "Khoa dược").OrderBy(p => p.TenKP).ToList();
            dskp.Insert(0, new KPhong { MaKP = 0, TenKP = "Tất cả" });
            cklKP.DataSource = dskp;
            cklKP.CheckAll();
            //for (int i = 0; i < cklKP.ItemCount; i++)
            //{
            //    if (cklKP.GetItemValue(i) != null && Convert.ToInt32(cklKP.GetItemValue(i)) == DungChung.Bien.MaKP)
            //        cklKP.SetItemChecked(i, true);
            //    else
            //        cklKP.SetItemChecked(i, false);
            //}
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
            data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            List<KPhong> _kpChon = new List<KPhong>();
            DateTime tungay = DungChung.Ham.NgayTu(deNgayTu.DateTime);
            DateTime denngay = DungChung.Ham.NgayDen(deNgayDen.DateTime);
            for (int i = 0; i < cklKP.ItemCount; i++)
            {
                if (cklKP.GetItemChecked(i))
                    _kpChon.Add(new KPhong { TenKP = cklKP.GetItemText(i), MaKP = cklKP.GetItemValue(i) == null ? 0 : Convert.ToInt32(cklKP.GetItemValue(i)) });
            }
            var qnxt0 = (from nhapd in data.NhapDs
                         join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                         where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3))
                         select new { nhapd.TraDuoc_KieuDon, nhapd.XuatTD, nhapd.MaKP, nhapd.IDNhap, nhapdct.MaDV, nhapdct.DonGiaCT, nhapdct.VAT, nhapdct.IDDTBN, nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX, ThanhTienCT = (nhapd.KieuDon == 1 && nhapd.PLoai == 1) ? ((double)nhapdct.SoLuongN * (double)nhapdct.DonGiaCT) : 0 }).Where(p => p.NgayNhap != null).ToList();
            //var dichvu = (from dv in data.DichVus
            //              join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
            //              join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom                       
            //              select new { dv.MaDV, dv.TenDV, tn.TenTN, nhomdv.TenNhom, tn.STT, dv.MaCC, dv.DonVi, dv.HamLuong, dv.TenHC, dv.MaTam, dv.SoDK, dv.NuocSX, dv.DuongD }).ToList();
            var qnxt1 = (from nd in qnxt0
                         join kp in _kpChon on nd.MaKP equals kp.MaKP
                         group nd by new { nd.IDNhap, nd.PLoai, nd.KieuDon, nd.TraDuoc_KieuDon, nd.NgayNhap } into kq
                         select new
                         {
                             kq.Key.IDNhap,
                             NgayNhap = kq.Key.NgayNhap.Value.Date,
                             NhapHD_SL = kq.Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.SoLuongN),// nhập theo hóa đơn số lượng
                             NhapHD_TT = (kq.Where(p => p.PLoai == 1 && p.KieuDon == 1).Count() > 0) ? ((double)kq.Where(p => p.PLoai == 1 && p.KieuDon == 1).Sum(p => p.ThanhTienCT) * ((double)kq.First().VAT / 100 + 1)) : 0,// nhập theo hóa đơn thành tiền

                             NhapKhac_SL = kq.Where(p => p.PLoai == 1 && p.KieuDon != 1 && p.KieuDon != 2).Sum(p => p.SoLuongN),// không phải nhập theo hóa đơn
                             NhapKhac_TT = kq.Where(p => p.PLoai == 1 && p.KieuDon != 1 && p.KieuDon != 2).Sum(p => p.ThanhTienN),

                             Xuat_SL = kq.Sum(p => p.SoLuongX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.SoLuongN),
                             Xuat_TT = kq.Sum(p => p.ThanhTienX) - kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Sum(p => p.ThanhTienN),

                         }).ToList();
            List<BC> list = new List<BC>();
            for (DateTime dt = tungay; dt <= denngay; dt = dt.AddDays(1))
            {
                DateTime dt_den = DungChung.Ham.NgayDen(dt);
                BC bc = new BC();
                bc.NgayNhap = dt;
                bc.TonDK_SL = qnxt1.Where(p => p.NgayNhap < dt).Sum(p => p.NhapHD_SL) + qnxt1.Where(p => p.NgayNhap < dt).Sum(p => p.NhapKhac_SL) - qnxt1.Where(p => p.NgayNhap < dt).Sum(p => p.Xuat_SL);
                bc.TonDK_TT = qnxt1.Where(p => p.NgayNhap < dt).Sum(p => p.NhapHD_TT) + qnxt1.Where(p => p.NgayNhap < dt).Sum(p => p.NhapKhac_TT) - qnxt1.Where(p => p.NgayNhap < dt).Sum(p => p.Xuat_TT);
                bc.NhapHD_SL = qnxt1.Where(p => p.NgayNhap >= dt && p.NgayNhap <= dt_den).Sum(p => p.NhapHD_SL);// nhập theo hóa đơn số lượng
                bc.NhapHD_TT = qnxt1.Where(p => p.NgayNhap >= dt && p.NgayNhap <= dt_den).Sum(p => p.NhapHD_TT);
                bc.NhapKhac_SL = qnxt1.Where(p => p.NgayNhap >= dt && p.NgayNhap <= dt_den).Sum(p => p.NhapKhac_SL);// không phải nhập theo hóa đơn
                bc.NhapKhac_TT = qnxt1.Where(p => p.NgayNhap >= dt && p.NgayNhap <= dt_den).Sum(p => p.NhapKhac_TT);
                bc.Xuat_SL = qnxt1.Where(p => p.NgayNhap >= dt && p.NgayNhap <= dt_den).Sum(p => p.Xuat_SL);
                bc.Xuat_TT = qnxt1.Where(p => p.NgayNhap >= dt && p.NgayNhap <= dt_den).Sum(p => p.Xuat_TT);
                bc.TonCK_SL = qnxt1.Where(p => p.NgayNhap <= dt_den).Sum(p => p.NhapHD_SL) + qnxt1.Where(p => p.NgayNhap <= dt_den).Sum(p => p.NhapKhac_SL) - qnxt1.Where(p => p.NgayNhap <= dt_den).Sum(p => p.Xuat_SL);
                bc.TonCK_TT = qnxt1.Where(p => p.NgayNhap <= dt_den).Sum(p => p.NhapHD_TT) + qnxt1.Where(p => p.NgayNhap <= dt_den).Sum(p => p.NhapKhac_TT) - qnxt1.Where(p => p.NgayNhap <= dt_den).Sum(p => p.Xuat_TT);
                list.Add(bc);
            }


            //var qnxt2 = (from nd in qnxt1                        
            //             group nd by new { nd.NgayNhap } into kq
            //             select new BC
            //             {

            //                 NgayNhap = kq.Key.NgayNhap,
            //                 TonDK_SL = kq.Where(p=>p.NgayNhap < tungay).Sum(p=>p.NhapHD_SL) + kq.Where(p=>p.NgayNhap < tungay).Sum(p=>p.NhapKhac_SL) - kq.Where(p=>p.NgayNhap < tungay).Sum(p=>p.Xuat_SL),
            //                 TonDK_TT = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.NhapHD_TT) + kq.Where(p => p.NgayNhap < tungay).Sum(p => p.NhapKhac_TT) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.Xuat_TT),
            //                 NhapHD_SL = kq.Where(p=>p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.NhapHD_SL),// nhập theo hóa đơn số lượng
            //                 NhapHD_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.NhapHD_TT),
            //                 NhapKhac_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.NhapKhac_SL),// không phải nhập theo hóa đơn
            //                 NhapKhac_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.NhapKhac_TT),
            //                 Xuat_SL = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.Xuat_SL),
            //                 Xuat_TT = kq.Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.Xuat_TT),
            //                 TonCK_SL = kq.Where(p => p.NgayNhap < denngay).Sum(p => p.NhapHD_SL) + kq.Where(p => p.NgayNhap < denngay).Sum(p => p.NhapKhac_SL) - kq.Where(p => p.NgayNhap < denngay).Sum(p => p.Xuat_SL),
            //                 TonCK_TT = kq.Where(p => p.NgayNhap < denngay).Sum(p => p.NhapHD_TT) + kq.Where(p => p.NgayNhap < denngay).Sum(p => p.NhapKhac_TT) - kq.Where(p => p.NgayNhap < denngay).Sum(p => p.Xuat_TT),


            //             }).Where(p=>p.NgayNhap >= tungay && p.NgayNhap <= denngay).Where(p=>p.TonDK_SL != 0 || p.NhapHD_SL != 0 || p.NhapKhac_SL != 0 || p.Xuat_SL != 0).OrderBy(p=>p.NgayNhap).ToList();

            BaoCao.rep_BCNXT_VAT rep = new BaoCao.rep_BCNXT_VAT();
            frmIn frm2 = new frmIn();
            rep.lblNgayThang.Text = "Từ ngày " + tungay.ToString("dd/MM/yyyy") + " đến ngày " + denngay.ToString("dd/MM/yyyy");
            rep.DataSource = list.Where(p => p.TonDK_SL != 0 || p.NhapHD_SL != 0 || p.NhapKhac_SL != 0 || p.Xuat_SL != 0).OrderBy(p => p.NgayNhap).ToList();
            rep.BindingData();
            rep.CreateDocument();
            frm2.prcIN.PrintingSystem = rep.PrintingSystem;
            frm2.ShowDialog();

        }
        public class BC
        {

            public DateTime NgayNhap { get; set; }

            public double TonDK_SL { get; set; }

            public double TonDK_TT { get; set; }

            public double NhapHD_TT { get; set; }

            public double NhapKhac_SL { get; set; }

            public double NhapKhac_TT { get; set; }

            public double NhapHD_SL { get; set; }

            public double Xuat_SL { get; set; }

            public double Xuat_TT { get; set; }

            public double TonCK_TT { get; set; }

            public double TonCK_SL { get; set; }
        }

        private void cklKP_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.State == CheckState.Checked)
                    cklKP.CheckAll();
                else
                    cklKP.UnCheckAll();
            }
        }
    }
}