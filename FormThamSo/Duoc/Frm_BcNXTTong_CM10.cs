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
    public partial class Frm_BcNXTTong_CM10 : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXTTong_CM10()
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
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKho.Focus();
                return false;
            }
            if (lupPL.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Phân loại");
                lupPL.Focus();
                return false;
            }
            if (chkTheoHD.Checked)
            {
                if (lupNhaCC.EditValue == null)
                {
                    MessageBox.Show("Bạn chưa chọn nhà cung cấp");
                    lupNhaCC.Focus();
                    return false;
                }
            }

            return true;
        }
        private void Frm_BcNXTTong_CM10_Load(object sender, EventArgs e)
        {
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            var nhacc = data.NhaCCs.OrderBy(p => p.TenCC).ToList();
            lupNhaCC.Properties.DataSource = nhacc;
            lupKho.Properties.DataSource = q.ToList();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
            NhomDV n = new NhomDV();
            n.TenNhomCT = " Tất cả";
            n.IDNhom = 99999;
            n.TenNhom = " Tất cả";
            List<NhomDV> _lNhomDV = new List<NhomDV>();
            _lNhomDV.Add(n);
            _lNhomDV.AddRange(data.NhomDVs.Where(p => p.Status == 1).ToList());
            lupPL.Properties.DataSource = _lNhomDV;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            string tennhom = "";
            if (lupPL.EditValue != null)
                tennhom = lupPL.EditValue.ToString();
            tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
            denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

            frmIn frm = new frmIn();
            BaoCao.Rep_BcNXTTong_CM10 rep = new BaoCao.Rep_BcNXTTong_CM10(chkHienThi.Checked);
            if (KTtaoBcNXT())
            {
                string _macc = "";
                if (lupNhaCC.EditValue != null)
                    _macc = lupNhaCC.EditValue.ToString();
                if (!string.IsNullOrEmpty(_macc))
                {

                    rep.TuNgay.Value = dateTuNgay.Text;
                    rep.DenNgay.Value = dateDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                    int _kho = 0;
                    if (lupKho.EditValue != null)
                        _kho = Convert.ToInt32( lupKho.EditValue);
                    rep.MaKP.Value = _kho;
                    var qtenkho = (from kp in data.KPhongs
                                   join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                                   where (nhapd.MaKP == _kho)
                                   select new { kp.TenKP }).ToList();

                    if (qtenkho.Count > 0)
                    {
                        rep.TieuDe.Value = ("báo cáo nhập xuất tồn " + lupPL.Text + " " + qtenkho.First().TenKP).ToUpper();
                    }

                    var qnxt =
                        (from nhapd in data.NhapDs
                         join nhapdct in data.NhapDcts.Where(p => p.MaCC != _macc) on nhapd.IDNhap equals nhapdct.IDNhap
                         join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                         join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                         join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                         join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                         where (kp.MaKP == _kho)
                         where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3))
                         group new { nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhapdct.SoLo, nhomdv.TenNhomCT, nhomdv.TenNhom, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, dv.NuocSX, dv.DonVi, nhapdct.DonGia } into kq
                         select new
                         {
                             kq.Key.SoLo,
                             MaDV = kq.Key.MaDV,
                             TenNhomDV = kq.Key.TenNhom,
                             TenTieuNhomDV = kq.Key.TenTN,
                             TenDV = kq.Key.TenDV,
                             DVT = kq.Key.DonVi,
                             kq.Key.TenNhomCT,
                             DonGia = kq.Key.DonGia,

                             TonDKSL = (kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX)),
                             TonDKTT = (kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX)),

                             NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                             NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                             XuatXaSL = kq.Where(p => p.nhapd.KieuDon == 3).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                             xuatXaTT = kq.Where(p => p.nhapd.KieuDon == 3).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                             XuatKhoSL = kq.Where(p => p.nhapd.KieuDon == 2).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                             xuatKhoTT = kq.Where(p => p.nhapd.KieuDon == 2).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                             TongXuatSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                             TongXuatTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                             TonCKSL = (kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX)),
                             TonCKTT = (kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX)),
                             XuatKhacSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon != 2 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.SoLuongX),
                             XuatKhacTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon != 2 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.ThanhTienX)
                         }).ToList().OrderBy(p => p.TenDV).ToList();

                    double TT = 0;

                    if (lupPL.Text == " Tất cả")
                    {
                        TT= qnxt.Sum(p => p.TonCKTT);
                        rep.TongTien.Value =DungChung.Ham.DocTienBangChu(TT,"đồng.");
                        rep.DataSource = qnxt.OrderBy(p => p.TenDV).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        qnxt = qnxt.Where(p => p.TenNhomCT.Contains(tennhom)).ToList();
                        TT = qnxt.Sum(p => p.TonCKTT);
                        rep.TongTien.Value = DungChung.Ham.DocTienBangChu(TT, "đồng.");
                        rep.DataSource = qnxt.OrderBy(p => p.TenDV).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }

                }
                else
                {
                    rep.TuNgay.Value = dateTuNgay.Text;
                    rep.DenNgay.Value = dateDenNgay.Text;
                    rep.TuNgayDenNgay.Value = "Từ ngày " + dateTuNgay.Text + " Đến ngày " + dateDenNgay.Text;
                    int _kho = 0;
                    if (lupKho.EditValue != null)
                        _kho = Convert.ToInt32( lupKho.EditValue);
                    rep.MaKP.Value = _kho;
                    var qtenkho = (from kp in data.KPhongs
                                   join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                                   where (nhapd.MaKP == _kho)
                                   select new { kp.TenKP }).ToList();

                    if (qtenkho.Count > 0)
                    {
                        rep.TieuDe.Value = ("báo cáo nhập xuất tồn " + lupPL.Text + " " + qtenkho.First().TenKP).ToUpper();
                    }

                    var qnxt =
                        (from nhapd in data.NhapDs
                         join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                         join kp in data.KPhongs on nhapd.MaKP equals kp.MaKP
                         join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                         join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                         join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                         where (kp.MaKP == _kho)
                         where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3))
                         group new { nhomdv, tieunhomdv, dv, nhapd, nhapdct } by new { nhomdv.TenNhom, nhomdv.TenNhomCT, tieunhomdv.TenTN, dv.MaDV, dv.TenDV, dv.NuocSX, dv.DonVi, nhapdct.DonGia } into kq
                         select new
                         {
                             MaDV = kq.Key.MaDV,
                             TenNhomDV = kq.Key.TenNhom,
                             TenTieuNhomDV = kq.Key.TenTN,
                             TenDV = kq.Key.TenDV,
                             DVT = kq.Key.DonVi,
                             DonGia = kq.Key.DonGia,
                             kq.Key.TenNhomCT,
                             TonDKSL = (kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.SoLuongX)),
                             TonDKTT = (kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < tungay).Sum(p => p.nhapdct.ThanhTienX)),

                             NhapTKSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongN),
                             NhapTKTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienN),

                             XuatXaSL = kq.Where(p => p.nhapd.KieuDon == 3).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                             xuatXaTT = kq.Where(p => p.nhapd.KieuDon == 3).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                             XuatKhoSL = kq.Where(p => p.nhapd.KieuDon == 2).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                             xuatKhoTT = kq.Where(p => p.nhapd.KieuDon == 2).Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),

                             TongXuatSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.SoLuongX),
                             TongXuatTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Sum(p => p.nhapdct.ThanhTienX),
                             TonCKSL = (kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.SoLuongX)),
                             TonCKTT =  (kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX)),
                             XuatKhacSL = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon != 2 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.SoLuongX),
                             XuatKhacTT = kq.Where(p => p.nhapd.NgayNhap >= tungay).Where(p => p.nhapd.NgayNhap <= denngay).Where(p => p.nhapd.KieuDon != 2 && p.nhapd.KieuDon != 3).Sum(p => p.nhapdct.ThanhTienX)
                         }).ToList().OrderBy(p => p.TenDV).ToList();

                    double TT = 0;
                    if (lupPL.Text== (" Tất cả"))
                    {
                        TT = qnxt.Sum(p => p.TonCKTT);
                        rep.TongTien.Value = DungChung.Ham.DocTienBangChu(TT, "đồng.");
                        rep.DataSource = qnxt.OrderBy(p => p.TenDV).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else 
                    {
                        qnxt = qnxt.Where(p => p.TenNhomCT.Contains(tennhom)).ToList();
                        TT = qnxt.Sum(p => p.TonCKTT);
                        rep.TongTien.Value = DungChung.Ham.DocTienBangChu(TT, "đồng.");
                        rep.DataSource = qnxt.OrderBy(p => p.TenDV).ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkTheoHD_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTheoHD.Checked)
            {
                lupNhaCC.Visible = true;
                labNhaCC.Visible = true;
            }
            else
            {
                lupNhaCC.Visible = false;
                labNhaCC.Visible = false;
            }
        }
    }
}