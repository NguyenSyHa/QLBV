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
    public partial class frmTsBcNXTXuat_TH : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBcNXTXuat_TH()
        {
            InitializeComponent();
        }
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

            else return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            int PLxuat = -1;

            if (KTtaoBcNXT())
            {
                if (chkInTX.Checked == false)
                {
                    QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
                    //PLxuat = radHinhthucx.SelectedIndex;
                    var dsnd = data.NhapDs.Where(p => p.KieuDon == PLxuat).Select(p => p.MaKPnx).Distinct().ToList();
                    //string k1 = "", k2 = "", k3 = "", k4 = "";
                    string t1 = "", t2 = "", t3 = "", t4 = "";
                    //int i = 0;
                    //i = dsnd.Count;
                    //string[] a = new string[30];
                    //string[] c = new string[30];
                    //for (int j = 0; j < 30; j++)
                    //{
                    //    a[j] = "";
                    //}
                    //int k = 0;
                    //foreach (var b in dsnd)
                    //{
                    //    a[k] = b;
                    //    var ten = data.KPhongs.Where(p => p.MaKP== (b)).ToList();
                    //    if (ten.Count > 0)
                    //        c[k] = ten.First().TenKP;
                    //    k++;
                    //}
                    //k1 = a[0];
                    //k2 = a[1];
                    //k3 = a[2];
                    //k4 = a[3];
                    //t1 = c[0];
                    //t2 = c[1];
                    //t3 = c[2];
                    //t4 = c[3];
                    int p1 = 0, p2 = 1, p3 = 2, p4 = 3;
                    int _kho = 0;
                    if (lupKho.EditValue != null)
                        _kho = Convert.ToInt32( lupKho.EditValue);
                    string _nhacc = "";
                    if (lupNhaCC.EditValue != null)
                        _nhacc = lupNhaCC.EditValue.ToString();
                    tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                    denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                    frmIn frm = new frmIn();
                    BaoCao.repBcNXTXuat_ct rep = new BaoCao.repBcNXTXuat_ct();

                    rep.Ten1.Value = "Xuất nội trú";
                    rep.Ten2.Value = "Xuất ngoại trú";
                    rep.Ten3.Value = "Xuất khác";
                    rep.Ten4.Value = t4;
                    rep.TuNgay.Value = dateTuNgay.Text;
                    rep.DenNgay.Value = dateDenNgay.Text;
                    rep.Kho.Value = _kho;
                    var qtenkho = (from kp in data.KPhongs
                                   join nhapd in data.NhapDs on kp.MaKP equals nhapd.MaKP
                                   where (nhapd.MaKP == _kho)
                                   select new { kp.TenKP }).ToList();
                    if (qtenkho.Count > 0)
                    {
                        rep.Kho.Value = qtenkho.First().TenKP;
                    }
                    var qtenncc = (from nhapd in data.NhapDs
                                   join nhacc in data.NhaCCs on nhapd.MaCC equals nhacc.MaCC
                                   where (nhacc.MaCC == _nhacc)
                                   select new { nhacc.TenCC }).ToList();
                    if (qtenncc.Count > 0)
                    {
                        rep.NhaCC.Value = qtenncc.First().TenCC;
                    }
                    if (!string.IsNullOrEmpty(_nhacc))
                    {
                        var qnxt = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.MaKP== (_kho)).Where(p => p.KieuDon == PLxuat)
                                    join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    join dv in data.DichVus.Where(p => p.MaCC == _nhacc) on nhapdct.MaDV equals dv.MaDV
                                    join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                    //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                    //where (kp.PLoai == "Khoa dược")                           
                                    group new { dv, nhapd, nhapdct } by new { nhapd.MaKP, nhomdv.TenNhom, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia } into kq
                                    select new
                                    {
                                        MaKP = kq.Key.MaKP,
                                        //TenKP = kq.Key.TenKP,
                                        TenNhomDuoc = kq.Key.TenNhom,
                                        //TenTieuNhomDuoc = kq.Key.TenTN,
                                        TenHamLuong = kq.Key.TenDV,
                                        DonVi = kq.Key.DonVi,
                                        DonGia = kq.Key.DonGia,

                                        XuatNoiTruSL = kq.Where(p => p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongX),
                                        XuatNoiTruTT = kq.Where(p => p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienX),

                                        XuatNgoaiTruSL = kq.Where(p => p.nhapd.KieuDon == 0 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == 3).Sum(p => p.nhapdct.SoLuongX),
                                        XuatNgoaiTruTT = kq.Where(p => p.nhapd.KieuDon == 0 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == 3).Sum(p => p.nhapdct.ThanhTienX),

                                        SL3 = kq.Where(p => p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6).Sum(p => p.nhapdct.SoLuongX),
                                        TT3 = kq.Where(p => p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6).Sum(p => p.nhapdct.ThanhTienX),

                                        //SL4 = kq.Sum(p => p.nhapdct.SoLuongX),
                                        //TT4 = kq.Sum(p => p.nhapdct.ThanhTienX),

                                        XuatTKTongSL = kq.Sum(p => p.nhapdct.SoLuongX),
                                        XuatTKTongTT = kq.Sum(p => p.nhapdct.ThanhTienX),

                                    }).ToList().OrderBy(p => p.TenHamLuong).ToList();


                        rep.DataSource = qnxt.OrderBy(p => p.TenHamLuong).ToList();

                        //rep.DataSource = qnxt.ToList();
                        rep.BindingData();
                        rep.CreateDocument();
                        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                        frm.ShowDialog();
                    }
                    else
                    {
                        var qnxt = (from nhapd in data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Where(p => p.MaKP== (_kho))
                                    join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                    join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                    join nhomdv in data.NhomDVs on dv.IDNhom equals nhomdv.IDNhom
                                    //join tieunhomdv in data.TieuNhomDVs on dv.IdTieuNhom equals tieunhomdv.IdTieuNhom
                                    //where (kp.PLoai == "Khoa dược")                           
                                    group new { dv, nhapd, nhapdct } by new { nhapd.MaKP, nhomdv.TenNhom, dv.TenDV, nhapdct.DonVi, nhapdct.DonGia } into kq
                                    select new
                                    {
                                        MaKP = kq.Key.MaKP,
                                        //TenKP = kq.Key.TenKP,
                                        TenNhomDuoc = kq.Key.TenNhom,
                                        //TenTieuNhomDuoc = kq.Key.TenTN,
                                        TenHamLuong = kq.Key.TenDV,
                                        DonVi = kq.Key.DonVi,
                                        DonGia = kq.Key.DonGia,

                                        XuatNoiTruSL = kq.Where(p => p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.SoLuongX),
                                        XuatNoiTruTT = kq.Where(p => p.nhapd.KieuDon == 1).Sum(p => p.nhapdct.ThanhTienX),

                                        XuatNgoaiTruSL = kq.Where(p => p.nhapd.KieuDon == 0 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == 3).Sum(p => p.nhapdct.SoLuongX),
                                        XuatNgoaiTruTT = kq.Where(p => p.nhapd.KieuDon == 0 || p.nhapd.KieuDon == 2 || p.nhapd.KieuDon == 3).Sum(p => p.nhapdct.ThanhTienX),

                                        SL3 = kq.Where(p => p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6).Sum(p => p.nhapdct.SoLuongX),
                                        TT3 = kq.Where(p => p.nhapd.KieuDon == 4 || p.nhapd.KieuDon == 5 || p.nhapd.KieuDon == 6).Sum(p => p.nhapdct.ThanhTienX),

                                        //SL4 = kq.Where(p => p.nhapd.MaKPnx == k4).Sum(p => p.nhapdct.SoLuongX),
                                        //TT4 = kq.Where(p => p.nhapd.MaKPnx == k4).Sum(p => p.nhapdct.ThanhTienX),

                                        XuatTKTongSL = kq.Sum(p => p.nhapdct.SoLuongX),
                                        XuatTKTongTT = kq.Sum(p => p.nhapdct.ThanhTienX),

                                    }).ToList().OrderBy(p => p.TenHamLuong).ToList();


                        rep.DataSource = qnxt.OrderBy(p => p.TenHamLuong).ToList();
                        //rep.DataSource = qnxt.ToList();
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

        private void frmTsBcNXTXuat_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai== ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();

            var qcc = from CC in data.NhaCCs select new { CC.MaCC, CC.TenCC };
            lupNhaCC.Properties.DataSource = qcc.ToList();
            dateTuNgay.DateTime = System.DateTime.Now;
            dateDenNgay.DateTime = System.DateTime.Now;
        }

    }
}