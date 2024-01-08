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
    public partial class Frm_BcNXTThuocYHCT : DevExpress.XtraEditors.XtraForm
    {
        public Frm_BcNXTThuocYHCT()
        {
            InitializeComponent();
        }
        private bool KTtaoBc()
        {
            if (dateTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                dateTuNgay.Focus();
                return false;
            }
            if (dateDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                dateDenNgay.Focus();
                return false;
            }
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho");
                lupKho.Focus();
                return false;
            }
            else return true;
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void Frm_BcNXTThuocYHCT_Load(object sender, EventArgs e)
        {
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
            dateTuNgay.Focus();
            var q = from TK in data.KPhongs.Where(p => p.PLoai == ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();
            List<string> _lNguonGoc = data.DichVus.Where(p => p.Status == 1 && p.DongY == 1).Select(p => p.NguonGoc).Distinct().ToList();
            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
            _lNguonGoc.Add(" Tất cả");
            _lNguonGoc = _lNguonGoc.OrderBy(p => p).ToList();
            cmbNG.Properties.Items.AddRange(_lNguonGoc);
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            if (KTtaoBc())
            {
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32(lupKho.EditValue);
                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                    _nhacc = lupNhaCC.EditValue.ToString();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;
                bool Checkht = ckcHienThi.Checked;
               

                //*********************************************
                var dvu = (from dv in data.DichVus.Where(p => p.DongY == 1)
                           join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                           select new { dv.NguonGoc, dv.TyLeBQ, dv.TyLeSD, dv.YCSD, dv.BPDung, dv.MaCC, dv.TenDV, dv.MaDV, dv.DonVi, tn.TenRG, tn.TenTN, dv.TinhTNhap, dv.TyLeSP }).ToList();
                List<TH> qnx2 = (from nhapd in data.NhapDs.Where(p => p.NgayNhap <= denngay).Where(p => p.MaKP == _kho).Where(p => p.PLoai == 1 || p.PLoai == 2 || p.PLoai == 3)
                                 join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                 select new TH
                                 {
                                     MaDV = nhapdct.MaDV ?? 0,
                                     NgayNhap = nhapd.NgayNhap.Value,
                                     PLoai = nhapd.PLoai.Value,
                                     DonGia = nhapdct.DonGia,
                                     DonGiaDY = nhapdct.DonGiaDY,
                                     SoLuongN = nhapd.PLoai == 1 ? nhapdct.SoLuongN : 0,
                                     SoLuongX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.SoLuongX : 0,
                                     ThanhTienN = nhapd.PLoai == 1 ? nhapdct.ThanhTienN : 0,
                                     ThanhTienX = (nhapd.PLoai == 2 || nhapd.PLoai == 3) ? nhapdct.ThanhTienX : 0,
                                     SoLuongDY = nhapdct.SoLuongDY,
                                     ThanhTienDY = nhapdct.ThanhTienDY,
                                     KieuDon = nhapd.KieuDon ?? 0
                                 }).ToList();
               

               
                //Xuất nhập tồn Tứ Kỳ:
                //Đơn giá: Đơn giá sau khi đã tính VAT;
                // đơn giá xuất: Đơn giá đã tính hư hao
                //- Tồn đầu kỳ:
                // + Số lượng: Không tính hư hao
                // + Thành tiền: SL * đơn giá nhập
                //- Nhập trong kỳ: 
                // + Số lượng: không tính hư hao
                // + Thành tiền: Số lượng * đơn giá nhập
                //- Xuất trong kỳ:
                // + Số lượng: Sau khi đã trừ hư hao
                // + Thành tiền: Số lượng * đơn giá xuất
                //- Tồn cuối kỳ:
                // + Số lượng: Tồn đầu kỳ + Nhập trong kỳ - xuất trong kỳ - hư hao
                // + thành tiền: Số lượng * đơn giá nhập

                bool bvTuKy = ckTuKy.Checked;


                var qnx = (from a in qnx2
                           join b in dvu on a.MaDV equals b.MaDV
                           group new { a, b } by new { b.NguonGoc, b.TenTN, a.MaDV, b.TenDV, b.TinhTNhap, b.YCSD, b.BPDung, b.TyLeBQ, b.TyLeSP, b.DonVi, a.DonGia, b.MaCC } into kq
                           select new
                           {
                               MaDV = kq.Key.MaDV,
                               MaCC = kq.Key.MaCC,
                               kq.Key.TenDV,
                               NguonGoc = kq.Key.NguonGoc,
                               TenTieuNhomDuoc = kq.Key.TenTN,
                               TenHamLuong = kq.Key.TenDV,
                               DonVi = kq.Key.DonVi,
                               TTNhap = kq.Key.TinhTNhap,
                               YCSD = kq.Key.YCSD,
                               BPSD = kq.Key.BPDung,
                               TLHH = kq.Key.TyLeSP,
                               TLBQ = kq.Key.TyLeBQ,
                               DonGiaDY = kq.Select(p => p.a.DonGiaDY).Max(),
                               DonGia = kq.Key.DonGia,
                               //TenKieuDon = kq.Key.KieuDon == 0 ? "Xuất ngoại trú (Theo đơn)" : (kq.Key.KieuDon == 1 ? "Xuất nội trú (theo phiếu lĩnh)" : (kq.Key.KieuDon == 2 ? "Xuất nội bộ" : (kq.Key.KieuDon == 3 ? "Xuất ngoài bệnh viện" : "Xuất nhân dân"))),
                               //KieuDon = kq.Key.KieuDon,

                               DKSoLuongN = kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.SoLuongN),
                               DKSoLuongDY = kq.Where(p => p.a.NgayNhap < tungay).Where(p => p.a.PLoai == 1).Sum(p => p.a.SoLuongDY),
                               DKSoLuongX = kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.SoLuongX),
                               HHDKSoLuongDY = kq.Where(p => p.a.NgayNhap < tungay).Where(p => p.a.PLoai == 2).Sum(p => p.a.SoLuongDY),// hư hao đầu kỳ

                               DKThanhTienN = kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.ThanhTienN),
                               DKThanhTienDY = kq.Where(p => p.a.NgayNhap < tungay).Where(p => p.a.PLoai == 1).Sum(p => p.a.ThanhTienDY),
                               DKThanhTienX = kq.Where(p => p.a.NgayNhap < tungay).Sum(p => p.a.ThanhTienX),
                               HHDKThanhTienDY = kq.Where(p => p.a.NgayNhap < tungay).Where(p => p.a.PLoai == 2).Sum(p => p.a.ThanhTienDY),// hư hao đầu kỳ


                               TKSoLuongN = kq.Where(p => p.a.NgayNhap >= tungay).Sum(p => p.a.SoLuongN),
                               TKSoLuongDY = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 1).Sum(p => p.a.SoLuongDY),
                               TKSoLuongX = kq.Where(p => p.a.NgayNhap >= tungay).Sum(p => p.a.SoLuongX),
                               HHTKSoLuongDY = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2).Sum(p => p.a.SoLuongDY),// hư hao trong kỳ

                               TKThanhTienN = kq.Where(p => p.a.NgayNhap >= tungay).Sum(p => p.a.ThanhTienN),
                               TKThanhTienDY = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 1).Sum(p => p.a.ThanhTienDY),
                               TKThanhTienX = kq.Where(p => p.a.NgayNhap >= tungay).Sum(p => p.a.ThanhTienX),
                               HHTKThanhTienDY = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2).Sum(p => p.a.ThanhTienDY),// hư hao trong kỳ

                               SLNgTruTK = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2 && p.a.KieuDon == 0).Sum(p => p.a.SoLuongX),
                               TTNgTruTK = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2 && p.a.KieuDon == 0).Sum(p => p.a.ThanhTienX),
                               SLNoiTruTK = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2 && p.a.KieuDon == 1).Sum(p => p.a.SoLuongX),
                               TTNoiTruTK = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2 && p.a.KieuDon == 1).Sum(p => p.a.ThanhTienX),
                               SLKhacTK = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2).Where(p => p.a.KieuDon != 0 && p.a.KieuDon != 1).Sum(p => p.a.SoLuongX),
                               TTKhacTK = kq.Where(p => p.a.NgayNhap >= tungay).Where(p => p.a.PLoai == 2).Where(p => p.a.KieuDon != 0 && p.a.KieuDon != 1).Sum(p => p.a.ThanhTienX),


                               CKSoLuongN = kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongN),
                               CKSoLuongDY = kq.Where(p => p.a.NgayNhap <= denngay).Where(p => p.a.PLoai == 1).Sum(p => p.a.SoLuongDY),
                               CKSoLuongX = kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.SoLuongX),
                               HHCKSoLuongDY = kq.Where(p => p.a.NgayNhap <= denngay).Where(p => p.a.PLoai == 2).Sum(p => p.a.SoLuongDY),// hư hao cuối kỳ

                               CKThanhTienN = kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.ThanhTienN),
                               CKThanhTienDY = kq.Where(p => p.a.NgayNhap <= denngay).Where(p => p.a.PLoai == 1).Sum(p => p.a.ThanhTienDY),
                               CKThanhTienX = kq.Where(p => p.a.NgayNhap <= denngay).Sum(p => p.a.ThanhTienX),
                               HHCKThanhTienDY = kq.Where(p => p.a.NgayNhap <= denngay).Where(p => p.a.PLoai == 2).Sum(p => p.a.ThanhTienDY),// hư hao cuối kỳ



                           }).ToList().Select(p => new BC
                           {
                               MaDV = p.MaDV,
                               MaCC = p.MaCC,
                               TenDV = p.TenDV,
                               NguonGoc = p.NguonGoc,
                               TenTieuNhomDuoc = p.TenTieuNhomDuoc,
                               //KieuDon = p.KieuDon,
                               //TenKieuDon = p.TenKieuDon,
                               TenHamLuong = p.TenDV,
                               DonVi = p.DonVi,
                               TTNhap = p.TTNhap,
                               YCSD = p.YCSD,
                               BPSD = p.BPSD,
                               TLHH = p.TLHH ?? 0,
                               TLBQ = p.TLBQ ?? 0,
                               DonGia = p.DonGia,
                               DonGiaDY = p.DonGiaDY,
                               TonDKSL = (chkHH.Checked && bvTuKy == false) ? (p.DKSoLuongN - p.DKSoLuongX) : (p.DKSoLuongDY - p.DKSoLuongX - p.HHDKSoLuongDY),
                               TonDKTT = bvTuKy ? (p.DKSoLuongDY - p.DKSoLuongX - p.HHDKSoLuongDY) * p.DonGiaDY : (chkHH.Checked ? (p.DKThanhTienN - p.DKThanhTienX) : (p.DKThanhTienN - p.DKThanhTienX - p.HHDKThanhTienDY)),

                               NhapTKSL = (chkHH.Checked && bvTuKy == false) ? p.TKSoLuongN : p.TKSoLuongDY,
                               NhapTKTT = (chkHH.Checked && bvTuKy == false) ? p.TKThanhTienN : p.TKThanhTienDY,

                               XuatTKSL = (chkHH.Checked && bvTuKy == false) ? (p.TKSoLuongX + p.HHTKSoLuongDY) : (p.TKSoLuongX),
                               XuatTKTT = bvTuKy ? p.TKSoLuongX * p.DonGia : (chkHH.Checked ? (p.TKThanhTienX + p.HHTKThanhTienDY) : (p.TKThanhTienX)),

                               TonCKSL = (chkHH.Checked && bvTuKy == false) ? (p.CKSoLuongN - p.CKSoLuongX) : (p.CKSoLuongDY - p.CKSoLuongX - p.HHCKSoLuongDY),
                               TonCKTT = bvTuKy ? (p.CKSoLuongDY - p.CKSoLuongX - p.HHCKSoLuongDY) * p.DonGiaDY : (chkHH.Checked ? (p.CKThanhTienN - p.CKThanhTienX) : (p.CKThanhTienN - p.CKThanhTienX - p.HHCKThanhTienDY)),

                               SLNGTruTK = p.SLNgTruTK,
                               TTNGtruTK = p.TTNgTruTK,

                               SLNoiTruTK = p.SLNoiTruTK,
                               TTNoiTruTK = p.TTNoiTruTK,

                               SLKhacTK = p.SLKhacTK,
                               TTKhacTK = p.TTKhacTK

                           }).OrderBy(p => p.TenHamLuong).ToList();

                //Lấy gán đơn giá nhập cho xuất dược có đơn giá xuất và mã dịch vụ tương ứng
               // List<TH> qnx3 = qnx2.Where(p => p.PLoai == 1).ToList();

                //foreach (BC a in qnx)
                //{
                //    if (a.DonGiaDY == 0)
                //    {
                //        var qdg = qnx3.Where(p => p.MaDV == a.MaDV && p.DonGia == a.DonGia).FirstOrDefault();
                //        if (qdg != null)
                //            a.DonGiaDY = qdg.DonGiaDY;
                //    }
                //}
                if (Checkht)
                {

                    frmIn frm = new frmIn();

                    BaoCao.Rep_BcNXTThuocYHCT_01071 rep = new BaoCao.Rep_BcNXTThuocYHCT_01071();

                    rep.TuNgayDenNgay.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                    rep.DenNgay.Value = dateDenNgay.Text;
                    var qtenkho = data.KPhongs.Where(p => p.MaKP == _kho).Select(p => new { p.TenKP }).ToList();

                    if (qtenkho.Count > 0)
                    {
                        rep.Kho.Value = qtenkho.First().TenKP;
                    }
                    var qtenncc = data.NhaCCs.Where(p => p.MaCC == _nhacc).Select(p => new { p.TenCC }).ToList();

                    if (qtenncc.Count > 0)
                    {
                        rep.NhaCC.Value = qtenncc.First().TenCC;
                    }
                    rep.DataSource = qnx.Where(p => ckTuKy.Checked == true ? (p.TonCKSL > 2 || p.NhapTKSL != 0 || p.XuatTKSL != 0) : true).Where(p => (cmbNG.Text == " Tất cả" ? true : p.NguonGoc == cmbNG.Text)).Where(p => (_nhacc == "" ? true : p.MaCC == _nhacc)).OrderBy(p => p.TenTieuNhomDuoc);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
                else
                {
                    frmIn frm = new frmIn();

                    BaoCao.Rep_BcNXTThuocYHCT rep = new BaoCao.Rep_BcNXTThuocYHCT();

                    rep.TuNgayDenNgay.Value = "Từ ngày: " + tungay.ToString().Substring(0, 10) + " đến ngày " + denngay.ToString().Substring(0, 10);
                    rep.DenNgay.Value = dateDenNgay.Text;
                    var qtenkho = data.KPhongs.Where(p => p.MaKP == _kho).Select(p => new { p.TenKP }).ToList();

                    if (qtenkho.Count > 0)
                    {
                        rep.Kho.Value = qtenkho.First().TenKP;
                    }
                    var qtenncc = data.NhaCCs.Where(p => p.MaCC == _nhacc).Select(p => new { p.TenCC }).ToList();

                    if (qtenncc.Count > 0)
                    {
                        rep.NhaCC.Value = qtenncc.First().TenCC;
                    }
                    rep.DataSource = qnx.Where(p => ckTuKy.Checked == true ? (p.TonCKSL > 2 || p.NhapTKSL != 0 || p.XuatTKSL != 0) : true).Where(p => (cmbNG.Text == " Tất cả" ? true : p.NguonGoc == cmbNG.Text)).Where(p => (_nhacc == "" ? true : p.MaCC == _nhacc)).OrderBy(p => p.TenTieuNhomDuoc);
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                }
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckTuKy_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTuKy.Checked)
                chkHH.Checked = false;
        }

        private void chkHH_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHH.Checked)
                ckTuKy.Checked = false;
        }
        public class TH
        {
            public int MaDV { set; get; }
            public DateTime NgayNhap { set; get; }
            public int PLoai { set; get; }
            public double DonGia { set; get; }
            public double DonGiaDY { set; get; }
            public double SoLuongN { set; get; }
            public double SoLuongX { set; get; }
            public double ThanhTienN { set; get; }
            public double ThanhTienX { set; get; }
            public double SoLuongDY { set; get; }
            public double ThanhTienDY { set; get; }
            public int KieuDon { get; set; }

        }
        public class BC
        {
            public int MaDV { set; get; }
            //public int KieuDon { get; set; }
            //public string TenKieuDon { get; set; }
            public string MaCC { set; get; }
            public string TenDV { set; get; }
            public string NguonGoc { set; get; }
            public string TenTieuNhomDuoc { set; get; }
            public string TenHamLuong { set; get; }
            public string DonVi { set; get; }
            public string TTNhap { set; get; }
            public string YCSD { set; get; }
            public string BPSD { set; get; }
            public double TLHH { set; get; }
            public double TLBQ { set; get; }
            public double DonGia { set; get; }
            public double DonGiaDY { set; get; }
            public double TonDKSL { set; get; }
            public double TonDKTT { set; get; }
            public double NhapTKSL { set; get; }
            public double NhapTKTT { set; get; }
            public double XuatTKSL { set; get; }
            public double XuatTKTT { set; get; }
            public double TonCKSL { set; get; }
            public double TonCKTT { set; get; }

            public double SLNGTruTK { set; get; }
            public double SLNoiTruTK { set; get; }
            public double TTNGtruTK { set; get; }
            public double SLKhacTK { set; get; }
            public double TTNoiTruTK { set; get; }
            public double TTKhacTK { set; get; }
       
        }
    }
}