using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLBV.FormThamSo
{
    public partial class frmTsBCNXTHH_NT : DevExpress.XtraEditors.XtraForm
    {
        public frmTsBCNXTHH_NT()
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
            if (lupKho.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khoa phòng để in báo cáo");
                lupKho.Focus();
                return false;
            }
            else return true;
        }
        private void btnInBC_Click(object sender, EventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;


            if (KTtaoBcNXT())
            {
                int _kho = 0;
                if (lupKho.EditValue != null)
                    _kho = Convert.ToInt32(lupKho.EditValue);
                string _nhacc = "";
                if (lupNhaCC.EditValue != null)
                    _nhacc = lupNhaCC.EditValue.ToString();
                tungay = DungChung.Ham.NgayTu(dateTuNgay.DateTime);//dateTuNgay.DateTime;
                denngay = DungChung.Ham.NgayDen(dateDenNgay.DateTime);//dateDenNgay.DateTime;

                frmIn frm = new frmIn();
                BaoCao.RepBcNXT_NT rep = new BaoCao.RepBcNXT_NT();
                QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

                rep.TuNgay.Value = dateTuNgay.Text;
                rep.DenNgay.Value = dateDenNgay.Text;
                rep.Kho.Value = lupKho.Text;// _kho;
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

                if (_kho > 0)
                {
                    var qnxt2 = (from nhapd in data.NhapDs.Where(p => p.MaKP == _kho)
                                 join nhapdct in data.NhapDcts on nhapd.IDNhap equals nhapdct.IDNhap
                                 join dv in data.DichVus on nhapdct.MaDV equals dv.MaDV
                                 join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                                 join nhomdv in data.NhomDVs on tn.IDNhom equals nhomdv.IDNhom
                                 join ncc in data.NhaCCs on dv.MaCC equals ncc.MaCC into kq
                                 from kq1 in kq.DefaultIfEmpty()
                                 where ((nhapd.PLoai == 1) || (nhapd.PLoai == 2) || (nhapd.PLoai == 3))

                                 select new { nhapd.PLoai, nhapd.KieuDon, nhapd.NgayNhap, dv.MaCC, TenCC = kq1 == null ? "" : kq1.TenCC, tn.TenTN, nhomdv.TenNhom, dv.TenDV, dv.MaDV, dv.DonVi, nhapdct.DonGia, nhapdct.SoLuongN, nhapdct.SoLuongX, nhapdct.ThanhTienN, nhapdct.ThanhTienX })
                                 .ToList();
                    // var a1 = (from q in qnxt2 where q.PLoai == 3 select q).ToList();
                    var qnxt = (from a in qnxt2
                                group a by new { a.MaCC, a.TenCC, a.TenTN, a.TenNhom, a.TenDV, a.DonVi, a.DonGia, a.MaDV } into kq
                                select new
                                {
                                    kq.Key.MaCC,
                                    MaDV = kq.Key.MaDV,
                                    TenCC = kq.Key.TenCC,
                                    TenNhomDuoc = kq.Key.TenNhom,
                                    TenHamLuong = kq.Key.TenDV,
                                    DonVi = kq.Key.DonVi,
                                    DonGia = kq.Key.DonGia,
                                    //SoLo=kq.Key.SoLo,

                                    TonDKSL = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.SoLuongX),
                                    TonDKTT = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),
                                    //TonDKTTTong = kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienN)-kq.Where(p => p.NgayNhap < tungay).Sum(p => p.ThanhTienX),
                                    HuHaoSL = kq.Where(p => p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    HuHaoTT = kq.Where(p => p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                    NhapTKSL = kq.Where(p => p.PLoai == 1 && p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongN),
                                    NhapTKTT = kq.Where(p => p.PLoai == 1 && p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN),
                                    //  NhapTKTTTong = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienN),

                                    XuatNoiTruSL = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    xuatNoiTruTT = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                    // xuatNoiTruTTTong = kq.Where(p => p.KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                    XuatNgoaiTruSL = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    xuatNgoaiTruTT = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 0).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                    // xuatNgoaiTruTTTong = kq.Where(p => p.KieuDon == 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                    XuatKhacSL = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0 && p.KieuDon != 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    xuatKhacTT = kq.Where(p => p.PLoai == 2).Where(p => p.KieuDon != 0).Where(p => p.KieuDon != 1).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                    XuatTKTongSL = kq.Where(p => p.PLoai == 2 || p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.SoLuongX),
                                    xuatTKTongTT = kq.Where(p => p.PLoai == 2 || p.PLoai == 3).Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),
                                    //  xuatTKTongTTTong = kq.Where(p => p.NgayNhap >= tungay).Where(p => p.NgayNhap <= denngay).Sum(p => p.ThanhTienX),

                                    NhapTraSL = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.SoLuongN),
                                    NhapTraTT = kq.Where(p => p.PLoai == 1 && p.KieuDon == 2).Where(p => p.NgayNhap >= tungay && p.NgayNhap <= denngay).Sum(p => p.ThanhTienN),

                                    TonCKSL = kq.Where(p => p.NgayNhap < denngay).Sum(p => p.SoLuongN) - kq.Where(p => p.NgayNhap < denngay).Sum(p => p.SoLuongX),
                                    TonCKTT = kq.Where(p => p.NgayNhap < denngay).Sum(p => p.ThanhTienN) - kq.Where(p => p.NgayNhap < denngay).Sum(p => p.ThanhTienX),
                                    // TonCKTTTong = kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienN) - kq.Where(p => p.nhapd.NgayNhap < denngay).Sum(p => p.nhapdct.ThanhTienX)

                                }).Where(p => p.TonDKSL != 0 || p.TonCKSL != 0 || p.NhapTKSL != 0 || p.XuatTKTongSL != 0).ToList().OrderBy(p => p.TenHamLuong).ToList();
                    var _sq = (from a in qnxt
                               select new
                               {
                                   a.DonGia,
                                   a.DonVi,
                                   a.MaCC,
                                   a.TenCC,
                                   a.MaDV,
                                   HuHaoSL = a.HuHaoSL,
                                   HuHaoTT = a.HuHaoTT,
                                   NhapTKSL = a.NhapTKSL,
                                   NhapTKTT = a.NhapTKTT,
                                   a.TenHamLuong,
                                   a.TenNhomDuoc,
                                   TonCKSL = a.TonCKSL,
                                   TonCKTT = a.TonCKTT,
                                   TonDKSL = a.TonDKSL,
                                   TonDKTT = a.TonDKTT,
                                   XuatKhacSL = a.XuatKhacSL,
                                   xuatKhacTT = a.xuatKhacTT,
                                   XuatNoiTruSL = a.XuatNoiTruSL,
                                   xuatNoiTruTT = a.xuatNoiTruTT,
                                   XuatNgoaiTruSL = a.XuatNgoaiTruSL,
                                   xuatNgoaiTruTT = a.xuatNgoaiTruTT,
                                   NhapTraSL = a.NhapTraSL,
                                   NhapTraTT = a.NhapTraTT,
                                   XuatTKTongSL = a.XuatTKTongSL,
                                   xuatTKTongTT = a.xuatTKTongTT,
                               }).ToList();
                    if (!string.IsNullOrEmpty(_nhacc))
                    {
                        rep.DataSource = _sq.Where(p => p.MaCC == _nhacc).OrderBy(p => p.TenHamLuong).ToList();
                        #region xuat Excel
                        if (Xuatex.Checked)
                        {


                            COMExcel.Application exApp = new COMExcel.Application();
                            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                      COMExcel.XlWBATemplate.xlWBATWorksheet);
                            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                            //exSheet.Activate();
                            exSheet.Name = "BcNhapXuatTon";// gán tên sheet
                            int i = 1;
                            string[] _arr = new string[18] { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Xuất nội trú TK - SL", "Xuất nội trú TK - TT", "Xuất ngoại trú TK - SL", "Xuất ngoại trú TK - TT", "Xuất khác TK - SL", "Xuất khác trú TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            int k = 0;
                            var qexcel = qnxt.Where(p => p.MaCC == _nhacc).OrderBy(p => p.TenHamLuong).ToList();
                            foreach (var b in _arr)
                            {
                                k++;
                                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                r.Value2 = b.ToString();
                                r.Columns.AutoFit();
                            }
                            foreach (var a in qexcel)
                            {
                                i++;
                                COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                r1.Value2 = i - 1;
                                r1.Columns.AutoFit();
                                COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                r2.NumberFormat = "@";
                                if (a.TenHamLuong != null)
                                    r2.Value2 = a.TenHamLuong;
                                r2.Columns.AutoFit();
                                COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                r3.NumberFormat = "@";
                                r3.Value2 = a.DonVi;
                                r3.Columns.AutoFit();
                                COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                r4.NumberFormat = "0";
                                r4.Value2 = a.DonGia;
                                r4.Columns.AutoFit();
                                COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                r5.NumberFormat = "0";
                                r5.Value2 = a.TonDKSL;
                                r5.Columns.AutoFit();
                                COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                r6.NumberFormat = "0";
                                r6.Value2 = a.TonDKTT;
                                r6.Columns.AutoFit();
                                COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                r7.NumberFormat = "0";
                                r7.Value2 = a.NhapTKSL;
                                r7.Columns.AutoFit();
                                COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                r8.NumberFormat = "0";
                                r8.Value2 = a.NhapTKTT;
                                r8.Columns.AutoFit();
                                COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                r9.NumberFormat = "0";
                                r9.Value2 = a.XuatNoiTruSL;
                                r9.Columns.AutoFit();
                                COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                r10.NumberFormat = "0";
                                r10.Value2 = a.xuatNoiTruTT;
                                r10.Columns.AutoFit();
                                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                r11.NumberFormat = "0";
                                r11.Value2 = a.XuatNgoaiTruSL;
                                r11.Columns.AutoFit();
                                COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                r12.NumberFormat = "0";
                                r12.Value2 = a.xuatNgoaiTruTT;
                                r12.Columns.AutoFit();
                                COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                r13.NumberFormat = "0";
                                r13.Value2 = a.XuatKhacSL;
                                r13.Columns.AutoFit();
                                COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                r14.NumberFormat = "0";
                                r14.Value2 = a.xuatKhacTT;
                                r14.Columns.AutoFit();
                                COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                r15.NumberFormat = "0";
                                r15.Value2 = a.XuatTKTongSL;
                                r15.Columns.AutoFit();
                                COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                r16.NumberFormat = "0";
                                r16.Value2 = a.xuatTKTongTT;
                                r16.Columns.AutoFit();
                                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                r17.NumberFormat = "0";
                                r17.Value2 = a.TonCKSL;
                                r17.Columns.AutoFit();
                                COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                r18.NumberFormat = "0";
                                r18.Value2 = a.TonCKTT;
                                r18.Columns.AutoFit();

                            }
                            exApp.Visible = true;//Ẩn hiện chương trình
                            exQLBV.SaveAs("C:\\BcNXT.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                            //exQLBV.Close(false, false, false);
                            //exApp.Quit(); // thoát ứng dụng
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }
                        #endregion
                    }
                    else
                    {
                        rep.DataSource = _sq.ToList();
                        #region xuat Excel
                        if (Xuatex.Checked)
                        {


                            COMExcel.Application exApp = new COMExcel.Application();
                            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                      COMExcel.XlWBATemplate.xlWBATWorksheet);
                            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                            //exSheet.Activate();
                            exSheet.Name = "BcNhapXuatTon";// gán tên sheet
                            int i = 1;
                            string[] _arr = new string[18] { "STT", "TenThuoc", "DVT", "DonGia", "Tồn ĐK - SL", "Tồn ĐK - TT", "Nhập TK - Sl", "Nhập TK - TT", "Xuất nội trú TK - SL", "Xuất nội trú TK - TT", "Xuất ngoại trú TK - SL", "Xuất ngoại trú TK - TT", "Xuất khác TK - SL", "Xuất khác trú TK - TT", "Tổng xuất TK - SL", "Tổng xuất TK - TT", "Tồn CK - SL", "Tồn CK - TT" };
                            int k = 0;
                            var qexcel = qnxt.OrderBy(p => p.TenHamLuong).ToList();
                            foreach (var b in _arr)
                            {
                                k++;
                                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                                r.Value2 = b.ToString();
                                r.Columns.AutoFit();
                            }
                            foreach (var a in qexcel)
                            {
                                i++;
                                COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                                r1.Value2 = i - 1;
                                r1.Columns.AutoFit();
                                COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                                r2.NumberFormat = "@";
                                if (a.TenHamLuong != null)
                                    r2.Value2 = a.TenHamLuong;
                                r2.Columns.AutoFit();
                                COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                                r3.NumberFormat = "@";
                                r3.Value2 = a.DonVi;
                                r3.Columns.AutoFit();
                                COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                                r4.NumberFormat = "0";
                                r4.Value2 = a.DonGia;
                                r4.Columns.AutoFit();
                                COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                                r5.NumberFormat = "0";
                                r5.Value2 = a.TonDKSL;
                                r5.Columns.AutoFit();
                                COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                                r6.NumberFormat = "0";
                                r6.Value2 = a.TonDKTT;
                                r6.Columns.AutoFit();
                                COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                                r7.NumberFormat = "0";
                                r7.Value2 = a.NhapTKSL;
                                r7.Columns.AutoFit();
                                COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                                r8.NumberFormat = "0";
                                r8.Value2 = a.NhapTKTT;
                                r8.Columns.AutoFit();
                                COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                                r9.NumberFormat = "0";
                                r9.Value2 = a.XuatNoiTruSL;
                                r9.Columns.AutoFit();
                                COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                                r10.NumberFormat = "0";
                                r10.Value2 = a.xuatNoiTruTT;
                                r10.Columns.AutoFit();
                                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                                r11.NumberFormat = "0";
                                r11.Value2 = a.XuatNgoaiTruSL;
                                r11.Columns.AutoFit();
                                COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                                r12.NumberFormat = "0";
                                r12.Value2 = a.xuatNgoaiTruTT;
                                r12.Columns.AutoFit();
                                COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                                r13.NumberFormat = "0";
                                r13.Value2 = a.XuatKhacSL;
                                r13.Columns.AutoFit();
                                COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                                r14.NumberFormat = "0";
                                r14.Value2 = a.xuatKhacTT;
                                r14.Columns.AutoFit();
                                COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                                r15.NumberFormat = "0";
                                r15.Value2 = a.XuatTKTongSL;
                                r15.Columns.AutoFit();
                                COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                                r16.NumberFormat = "0";
                                r16.Value2 = a.xuatTKTongTT;
                                r16.Columns.AutoFit();
                                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                                r17.NumberFormat = "0";
                                r17.Value2 = a.TonCKSL;
                                r17.Columns.AutoFit();
                                COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                                r18.NumberFormat = "0";
                                r18.Value2 = a.TonCKTT;
                                r18.Columns.AutoFit();

                            }
                            exApp.Visible = true;//Ẩn hiện chương trình
                            exQLBV.SaveAs("C:\\BcNXT.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                                            null, null, false, false,
                                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                                            false, false, false, false, false);
                            //exQLBV.Close(false, false, false);
                            //exApp.Quit(); // thoát ứng dụng
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                        }
                        #endregion
                    }
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

        List<NhaCC> _lNCC = new List<NhaCC>();
        private void frmTsBCNXT_Load(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var q = from TK in data.KPhongs.Where(p => p.PLoai == ("Khoa dược")) select new { TK.TenKP, TK.MaKP };
            lupKho.Properties.DataSource = q.ToList();

            _lNCC = data.NhaCCs.ToList();
            _lNCC.Insert(0, new NhaCC { MaCC = "", TenCC = "Tất cả" });
            lupNhaCC.Properties.DataSource = _lNCC.ToList();
            dateDenNgay.DateTime = System.DateTime.Now;
            dateTuNgay.DateTime = System.DateTime.Now;
        }

        private void lupKho_EditValueChanged(object sender, EventArgs e)
        {

        }


    }
}