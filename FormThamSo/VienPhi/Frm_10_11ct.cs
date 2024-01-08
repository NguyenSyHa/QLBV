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
    public partial class Frm_10_11ct : DevExpress.XtraEditors.XtraForm
    {
        public Frm_10_11ct()
        {
            InitializeComponent();
        }
        private bool KTtaoBc()
        {

            if (lupTuNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu in báo cáo");
                lupTuNgay.Focus();
                return false;
            }
            if (lupDenNgay.EditValue == null)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc in báo cáo");
                lupDenNgay.Focus();
                return false;
            }
            else return true;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);


            DateTime ngaytu = System.DateTime.Now.Date;
            DateTime ngayden = System.DateTime.Now.Date;

            if (KTtaoBc())
            {
                ngaytu = DungChung.Ham.NgayTu(lupTuNgay.DateTime);
                ngayden = DungChung.Ham.NgayDen(lupDenNgay.DateTime);
                frmIn frm = new frmIn();
                BaoCao.Rep_10_11ct rep = new BaoCao.Rep_10_11ct();
                rep.Tungaydenngay.Value = "Từ ngày: " + ngaytu.ToString().Substring(0, 10) + " Đến ngày: " + ngayden.ToString().Substring(0, 10);
                int noitinh = 0;
                if (radMau10Mau11.SelectedIndex == 0)
                {
                    noitinh = 2;
                    rep.Mau.Value = "Mẫu số 10/BHYT";
                    rep.NoiNgoaiTinh.Value = "TỔNG HỢP CHI PHÍ KCB BHYT THANH TOÁN ĐA TUYẾN NỘI TỈNH";
                }
                else
                {
                    noitinh = 3;
                    rep.Mau.Value = "Mẫu số 11/BHYT";
                    rep.NoiNgoaiTinh.Value = "TỔNG HỢP CHI PHÍ KCB BHYT THANH TOÁN ĐA TUYẾN NGOẠI TỈNH";
                }
                var bvien = (from bv in dataContext.BenhViens select new { bv.MaBV, bv.TenBV, bv.TuyenBV, bv.HangBV }).ToList();
                var dtuong = (from dt in dataContext.DTuongs select dt).ToList();
                var dichvu = (from dv in dataContext.DichVus
                              join tnhom in dataContext.TieuNhomDVs on dv.IdTieuNhom equals tnhom.IdTieuNhom
                              join nhom in dataContext.NhomDVs on tnhom.IDNhom equals nhom.IDNhom
                              select new { dv.MaDV, nhom.TenNhomCT }).ToList();
                var qnt2 = (from bn in dataContext.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTinh == noitinh && p.MaKCB == DungChung.Bien.MaBV).Where(p => p.SThe.Length > 0)
                           
                            //join dt in dataContext.DTuongs on bn.SThe.Substring(0, 2) equals dt.MaDTuong
                            //join bv in dataContext.BenhViens on bn.MaCS equals bv.MaBV
                            join vp in dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                            join vpct in dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                         
                            join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                            where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                            where (vp.Duyet == 2 || vp.Duyet == 1)

                            select new
                            {
                                //nhom.TenNhomCT,
                                //bv.MaBV,
                                //dt.Nhom,
                                bn.Tuyen,
                                MaBNhan = bn.MaBNhan,
                                TenBNhan = bn.TenBNhan,
                                SThe = bn.SThe,
                                //TenBV = bv.TenBV,
                                MaCS = bn.MaCS,
                                NoiTru = bn.NoiTru,
                                NNhap = bn.NNhap,
                                NgayRa = rv.NgayRa,
                                TienBH = vpct.TienBH,
                                TienChenh = vpct.TienChenh,
                                ThanhTien = vpct.ThanhTien,
                                TienBN = vpct.TienBN,
                                // lấy để xuât Excel
                                HanBHTu = bn.HanBHTu,
                                HanBHDen = bn.HanBHDen,
                                NgayKham = vp.NgayTT,
                                DiaChi = bn.DChi,
                                // BenhKhac=kq.Key.BenhKhac,
                                NamSinh = bn.NamSinh,
                                GTinh = bn.GTinh,
                                Songay = rv.SoNgaydt,
                                MaICD = rv.MaICD,
                                MaKP = rv.MaKP, vpct.MaDV,
                               
                            }).OrderByDescending(p => p.TenBNhan).ToList();
                var qnt = (from a in qnt2
                           join dv in dichvu on a.MaDV equals dv.MaDV
                           join dt in dtuong on a.SThe.Substring(0, 2) equals dt.MaDTuong
                           join bv in bvien on a.MaCS equals bv.MaBV
                           group new { a ,dv,dt,bv} by new { a.DiaChi, dt.Nhom, a.MaBNhan, a.TenBNhan, a.NNhap, a.NamSinh, a.GTinh, a.SThe, a.HanBHTu, a.HanBHDen, a.MaCS, a.MaKP, bv.MaBV, bv.TenBV, a.Tuyen, a.MaICD, a.NgayRa, a.Songay } into kq
                           select new
                           {
                               Nhom = kq.Key.Nhom,
                               MaBNhan = kq.Key.MaBNhan,
                               TenBNhan = kq.Key.TenBNhan,
                               SThe = kq.Key.SThe,
                               TenCSBD = kq.Key.TenBV,
                               MaCS = kq.Key.MaCS,
                               NgayVao = kq.Key.NNhap,
                               NgayRa = kq.Key.NgayRa,
                               NgoaiTru = kq.Where(p => p.a.NoiTru == 0).Sum(p => p.a.TienBH) - kq.Where(p => p.a.NoiTru == 0).Sum(p => p.a.TienChenh),
                               NoiTru = kq.Where(p => p.a.NoiTru == 1).Sum(p => p.a.TienBH) - kq.Where(p => p.a.NoiTru == 1).Sum(p => p.a.TienChenh),
                               TongSoLuot = kq.Select(p => p.a.MaBNhan).Count(),
                               // lấy để xuât Excel
                               HanBHTu = kq.Key.HanBHTu,
                               HanBHDen = kq.Key.HanBHDen,
                               NgayKham = kq.Key.NgayRa,
                               DiaChi = kq.Key.DiaChi,
                               // BenhKhac = kq.Key.BenhKhac,
                               NamSinh = kq.Key.NamSinh,
                               GTinh = kq.Key.GTinh,
                               Songay = kq.Key.Songay,
                               MaICD = kq.Key.MaICD,
                               MaKP = kq.Key.MaKP,
                               Thuoc = kq.Where(p => p.dv.TenNhomCT.Contains("Thuốc")).Sum(p => p.a.ThanhTien),
                               CDHA = kq.Where(p => p.dv.TenNhomCT.Contains("CĐHA")).Sum(p => p.a.ThanhTien),
                               Congkham = kq.Where(p => p.dv.TenNhomCT.Contains("Ngày giường")).Sum(p => p.a.ThanhTien),
                               Xetnghiem = kq.Where(p => p.dv.TenNhomCT.Contains("xét nghiệm")).Sum(p => p.a.ThanhTien),
                               Mau = kq.Where(p => p.dv.TenNhomCT.Contains("máu")).Sum(p => p.a.ThanhTien),
                               TTPT = kq.Where(p => p.dv.TenNhomCT.Contains("thủ thuật")).Sum(p => p.a.ThanhTien),
                               VTYT = kq.Where(p => p.dv.TenNhomCT.Contains("vật tư")).Sum(p => p.a.ThanhTien),
                               DVKTC = kq.Where(p => p.dv.TenNhomCT.Contains("kỹ thuật")).Sum(p => p.a.ThanhTien),
                               ThuocKTCG = kq.Where(p => p.dv.TenNhomCT.Contains("Thải ghép")).Sum(p => p.a.ThanhTien),
                               CPVanchuyen = kq.Where(p => p.dv.TenNhomCT.Contains("Vận chuyển")).Sum(p => p.a.ThanhTien),
                               ThanhTien = kq.Sum(p => p.a.ThanhTien),
                               Tongchi = kq.Sum(p => p.a.ThanhTien),
                               Tongcong = kq.Sum(p => p.a.ThanhTien),
                               Nguoibenhchitra = kq.Sum(p => p.a.TienBN),
                               TongcongBHYT = kq.Sum(p => p.a.TienBH),
                           }).OrderByDescending(p => p.TenBNhan).ToList();
          
                if (qnt.Count > 0)
                {
                    rep.DataSource = qnt.ToList();
                    rep.BindingData();
                    rep.CreateDocument();
                    frm.prcIN.PrintingSystem = rep.PrintingSystem;
                    frm.ShowDialog();
                    #region xuat Excel
                    if (chkXuatExel.Checked)
                    {
                        COMExcel.Application exApp = new COMExcel.Application();
                        COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                                  COMExcel.XlWBATemplate.xlWBATWorksheet);
                        COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                        exSheet.Activate();
                        exSheet.Name = "bc10bhyt_005";// gán tên sheet
                        int i = 1;
                        string[] _arr = new string[43] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra",
                                                            "ngaydtr","t_tongchi","t_xn","t_cdha","t_thuoc","t_mau","t_pttt","t_vtytth","t_vtyttt","t_dvktc",
                                                            "t_ktg","t_kham","t_vchuyen","t_bnct","t_bhtt","t_ngoaids","lydo_vv","benhkhac","noikcb","khoa",
                                                            "thang_qt","nam_qt","gt_tu","gt_den","diachi","giamdinh","t_xuattoan","lydo_xt","t_datuyen",
                                                            "t_vuottran","loaikcb","noi_ttoan","sophieu","ma_khoa"};
                        int k = 0;
                        foreach (var b in _arr)
                        {
                            k++;
                            COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                            r.Value2 = b.ToString();
                            r.Columns.AutoFit();
                        }
                        foreach (var a in qnt)
                        {
                            i++;
                            COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                            r1.Value2 = i - 1;
                            r1.Columns.AutoFit();
                            COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                            r2.Value2 = a.TenBNhan;
                            r2.Columns.AutoFit();
                            COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                            r3.Value2 = a.NamSinh;
                            r3.Columns.AutoFit();
                            COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                            r4.Value2 = a.GTinh;
                            r4.Columns.AutoFit();
                            COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                            r5.Value2 = a.SThe;
                            r5.Columns.AutoFit();
                            COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                            r6.Value2 = a.MaCS;
                            r6.Columns.AutoFit();
                            COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                            r7.Value2 = a.MaICD;
                            r7.Columns.AutoFit();
                            COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                            if (a.NgayVao != null)
                            {
                                r8.Value2 = a.NgayVao.ToString().Substring(0, 10);
                                r8.Columns.AutoFit();
                            }
                            COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                            if (a.NgayRa != null)
                            {
                                r9.Value2 = a.NgayRa.ToString().Substring(0, 10);
                                r9.Columns.AutoFit();
                            }
                            COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                            r10.Value2 = a.Songay;
                            r10.Columns.AutoFit();
                            COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                            r11.Value2 = a.ThanhTien;
                            r11.Columns.AutoFit();
                            COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                            r12.Value2 = a.Xetnghiem;
                            r12.Columns.AutoFit();
                            COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                            r13.Value2 = a.CDHA;
                            r13.Columns.AutoFit();
                            COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                            r14.Value2 = a.Thuoc;
                            r14.Columns.AutoFit();
                            COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                            r15.Value2 = a.Mau;
                            r15.Columns.AutoFit();
                            COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                            r16.Value2 = a.TTPT;
                            r16.Columns.AutoFit();
                            COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                            r17.Value2 = a.VTYT;
                            r17.Columns.AutoFit();
                            COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                            r18.Value2 = 0;
                            r18.Columns.AutoFit();
                            COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                            r19.Value2 = a.DVKTC;
                            r19.Columns.AutoFit();
                            COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                            r20.Value2 = a.ThuocKTCG;
                            r20.Columns.AutoFit();
                            COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                            r21.Value2 = a.Congkham;
                            r21.Columns.AutoFit();
                            COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                            r22.Value2 = a.CPVanchuyen;
                            r22.Columns.AutoFit();
                            COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                            r23.Value2 = a.Nguoibenhchitra;
                            r23.Columns.AutoFit();
                            COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                            r24.Value2 = a.TongcongBHYT;
                            r24.Columns.AutoFit();
                            COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                            r25.Value2 = a.CPVanchuyen;
                            r25.Columns.AutoFit();
                            COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                            r26.Value2 = 1;
                            r26.Columns.AutoFit();
                            COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                            r27.Value2 = " ";
                            r27.Columns.AutoFit();
                            COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 27];
                            r28.Value2 = DungChung.Bien.MaBV;
                            r28.Columns.AutoFit();
                            COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                            r29.Value2 = a.MaKP;
                            r29.Columns.AutoFit();
                            COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                            if (a.NgayKham != null)
                            {
                                r30.Value2 = a.NgayKham.ToString().Substring(3, 2);
                                r30.Columns.AutoFit();
                            }
                            COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];

                            if (a.NgayKham != null)
                            {
                                r31.Value2 = a.NgayKham.ToString().Substring(6, 4);
                                r31.Columns.AutoFit();
                            }
                            COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                            if (a.HanBHTu != null)
                            {
                                r32.Value2 = a.HanBHTu.ToString().Substring(0, 10);
                                r32.Columns.AutoFit();
                            }
                            COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                            if (a.HanBHDen != null)
                            {
                                r33.Value2 = a.HanBHDen.ToString().Substring(0, 10);
                                r33.Columns.AutoFit();
                            }
                            COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                            r34.Value2 = a.DiaChi;
                            r34.Columns.AutoFit();
                            COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                            r35.Value2 = "";
                            r35.Columns.AutoFit();
                            COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                            r36.Value2 = 0;
                            r36.Columns.AutoFit();
                            COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                            r37.Value2 = "";
                            r37.Columns.AutoFit();
                            COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                            r38.Value2 = 0;
                            r38.Columns.AutoFit();
                            COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                            r39.Value2 = 0;
                            r39.Columns.AutoFit();
                            COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                            r40.Value2 = "NOI";
                            r40.Columns.AutoFit();
                            COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                            r41.Value2 = "CSKCB";
                            r41.Columns.AutoFit();
                            COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                            r42.Value2 = a.MaBNhan;
                            r42.Columns.AutoFit();
                            COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                            r43.Value2 = a.MaKP;
                            r43.Columns.AutoFit();
                            COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                            r44.Value2 = "";
                            r44.Columns.AutoFit();

                        }
                        exApp.Visible = true;//Ẩn hiện chương trình
                        exQLBV.SaveAs("C:\\Bieu10_BHYT.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
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

                //if (radMau10Mau11.SelectedIndex == 1)
                //{
                //    rep.Mau.Value = "Mẫu số 11/BHYT";
                //    rep.NoiNgoaiTinh.Value = "TỔNG HỢP CHI PHÍ KCB BHYT THANH TOÁN ĐA TUYẾN NGOẠI TỈNH";

                //var qngt = (from bn in dataContext.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p => p.NoiTinh == 3).Where(p => p.SThe.Length > 0)
                //            //join kb in dataContext.BNKBs on bn.MaBNhan equals kb.MaBNhan
                //             join dt in dataContext.DTuongs on bn.SThe.Substring(0, 2) equals dt.MaDTuong
                //             join bv in dataContext.BenhViens on bn.MaCS equals bv.MaBV
                //             join vp in dataContext.VienPhis on bn.MaBNhan equals vp.MaBNhan
                //             join vpct in dataContext.VienPhicts.Where(p => p.TrongBH == 1) on vp.idVPhi equals vpct.idVPhi
                //             join rv in dataContext.RaViens on bn.MaBNhan equals rv.MaBNhan
                //             join dv in dataContext.DichVus on vpct.MaDV equals dv.MaDV
                //             join nhom in dataContext.NhomDVs on dv.IDNhom equals nhom.IDNhom
                //            where (vp.NgayTT >= ngaytu && vp.NgayTT <= ngayden)
                //            where (vp.Duyet == 2 || vp.Duyet == 1)
                //            group new { bn, rv, dv, vpct, nhom, vp, dt } by new { dt.Nhom, bn.MaBNhan, bn.TenBNhan, bn.DChi, bn.NamSinh, bn.GTinh, bn.SThe, bn.HanBHTu, bn.HanBHDen, bn.MaCS, bn.NNhap, bn.MaKP, bv.MaBV, bv.TenBV, bn.Tuyen, rv.MaICD, rv.NgayRa, rv.SoNgaydt,vp.NgayTT } into kq
                //            select new
                //             {
                //                 Nhom = kq.Key.Nhom,
                //                 MaBNhan = kq.Key.MaBNhan,
                //                 TenBNhan = kq.Key.TenBNhan,
                //                 SThe = kq.Key.SThe,
                //                 TenCSBD = kq.Key.TenBV,
                //                 MaCS = kq.Key.MaCS,
                //                 NgayVao = kq.Key.NNhap,
                //                 NgayRa = kq.Key.NgayRa,
                //                 NgoaiTru = kq.Where(p => p.bn.NoiTru == 0).Sum(p => p.vpct.TienBH) - kq.Where(p => p.bn.NoiTru == 0).Sum(p => p.vpct.TienChenh),
                //                 NoiTru = kq.Where(p => p.bn.NoiTru == 1).Sum(p => p.vpct.TienBH) - kq.Where(p => p.bn.NoiTru == 1).Sum(p => p.vpct.TienChenh),
                //                 TongSoLuot = kq.Select(p => p.bn.MaBNhan).Count(),
                //                 // lấy để xuât Excel
                //                 HanBHTu = kq.Key.HanBHTu,
                //                 HanBHDen = kq.Key.HanBHDen,
                //                 NgayKham = kq.Key.NgayTT,
                //                 DiaChi = kq.Key.DChi,
                //                 //BenhKhac = kq.Key.BenhKhac,
                //                 NamSinh = kq.Key.NamSinh,
                //                 GTinh = kq.Key.GTinh,
                //                 Songay = kq.Key.SoNgaydt,
                //                 MaICD = kq.Key.MaICD,
                //                 MaKP = kq.Key.MaKP,
                //                 Thuoc = kq.Where(p => p.nhom.TenNhom.Contains("Thuốc")).Sum(p => p.vpct.ThanhTien),
                //                 CDHA = kq.Where(p => p.nhom.TenNhom.Contains("CĐHA")).Sum(p => p.vpct.ThanhTien),
                //                 Congkham = kq.Where(p => p.nhom.TenNhom.Contains("Ngày giường")).Sum(p => p.vpct.ThanhTien),
                //                 Xetnghiem = kq.Where(p => p.nhom.TenNhom.Contains("xét nghiệm")).Sum(p => p.vpct.ThanhTien),
                //                 Mau = kq.Where(p => p.nhom.TenNhom.Contains("máu")).Sum(p => p.vpct.ThanhTien),
                //                 TTPT = kq.Where(p => p.nhom.TenNhom.Contains("thủ thuật")).Sum(p => p.vpct.ThanhTien),
                //                 VTYT = kq.Where(p => p.nhom.TenNhom.Contains("vật tư")).Sum(p => p.vpct.ThanhTien),
                //                 DVKTC = kq.Where(p => p.nhom.TenNhom.Contains("kỹ thuật")).Sum(p => p.vpct.ThanhTien),
                //                 ThuocKTCG = kq.Where(p => p.nhom.TenNhom.Contains("Thải ghép")).Sum(p => p.vpct.ThanhTien),
                //                 CPVanchuyen = kq.Where(p => p.nhom.TenNhom.Contains("Vận chuyển")).Sum(p => p.vpct.ThanhTien),
                //                 ThanhTien = kq.Sum(p => p.vpct.ThanhTien),
                //                 Tongchi = kq.Sum(p => p.vpct.ThanhTien),
                //                 Tongcong = kq.Sum(p => p.vpct.ThanhTien),
                //                 Nguoibenhchitra = kq.Sum(p => p.vpct.TienBN),
                //                 TongcongBHYT = kq.Sum(p => p.vpct.TienBH),
                //             }).OrderByDescending(p => p.TenBNhan).ToList();
                //    if (qngt.Count > 0)
                //    {
                //        rep.DataSource = qngt.ToList();
                //        rep.BindingData();
                //        rep.CreateDocument();
                //        frm.prcIN.PrintingSystem = rep.PrintingSystem;
                //        frm.ShowDialog();
                //        #region xuat Excel
                //        if (chkXuatExel.Checked)
                //        {
                //            COMExcel.Application exApp = new COMExcel.Application();
                //            COMExcel.Workbook exQLBV = exApp.Workbooks.Add(
                //                      COMExcel.XlWBATemplate.xlWBATWorksheet);
                //            COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exQLBV.Worksheets[1];
                //            exSheet.Activate();
                //            exSheet.Name = "bc11bhyt_005";// gán tên sheet
                //            int i = 1;
                //            string[] _arr = new string[43] { "stt", "hoten", "namsinh", "gioitinh", "mathe", "ma_dkbd", "mabenh", "ngay_vao", "ngay_ra",
                //                                            "ngaydtr","t_tongchi","t_xn","t_cdha","t_thuoc","t_mau","t_pttt","t_vtytth","t_vtyttt","t_dvktc",
                //                                            "t_ktg","t_kham","t_vchuyen","t_bnct","t_bhtt","t_ngoaids","lydo_vv","benhkhac","noikcb","khoa",
                //                                            "thang_qt","nam_qt","gt_tu","gt_den","diachi","giamdinh","t_xuattoan","lydo_xt","t_datuyen",
                //                                            "t_vuottran","loaikcb","noi_ttoan","sophieu","ma_khoa"};
                //            int k = 0;
                //            foreach (var b in _arr)
                //            {
                //                k++;
                //                COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, k];
                //                r.Value2 = b.ToString();
                //                r.Columns.AutoFit();
                //            }
                //            foreach (var a in qngt)
                //            {
                //                i++;
                //                COMExcel.Range r1 = (COMExcel.Range)exSheet.Cells[i, 1];
                //                r1.Value2 = i - 1;
                //                r1.Columns.AutoFit();
                //                COMExcel.Range r2 = (COMExcel.Range)exSheet.Cells[i, 2];
                //                r2.Value2 = a.TenBNhan;
                //                r2.Columns.AutoFit();
                //                COMExcel.Range r3 = (COMExcel.Range)exSheet.Cells[i, 3];
                //                r3.Value2 = a.NamSinh;
                //                r3.Columns.AutoFit();
                //                COMExcel.Range r4 = (COMExcel.Range)exSheet.Cells[i, 4];
                //                r4.Value2 = a.GTinh;
                //                r4.Columns.AutoFit();
                //                COMExcel.Range r5 = (COMExcel.Range)exSheet.Cells[i, 5];
                //                r5.Value2 = a.SThe;
                //                r5.Columns.AutoFit();
                //                COMExcel.Range r6 = (COMExcel.Range)exSheet.Cells[i, 6];
                //                r6.Value2 = a.MaCS;
                //                r6.Columns.AutoFit();
                //                COMExcel.Range r7 = (COMExcel.Range)exSheet.Cells[i, 7];
                //                r7.Value2 = a.MaICD;
                //                r7.Columns.AutoFit();
                //                COMExcel.Range r8 = (COMExcel.Range)exSheet.Cells[i, 8];
                //                if (a.NgayVao != null)
                //                {
                //                    r8.Value2 = a.NgayVao.ToString().Substring(0, 10);
                //                    r8.Columns.AutoFit();
                //                }

                //                COMExcel.Range r9 = (COMExcel.Range)exSheet.Cells[i, 9];
                //                if (a.NgayRa != null)
                //                {
                //                    r9.Value2 = a.NgayRa.ToString().Substring(0, 10);
                //                    r9.Columns.AutoFit();
                //                }
                //                COMExcel.Range r10 = (COMExcel.Range)exSheet.Cells[i, 10];
                //                r10.Value2 = a.Songay;
                //                r10.Columns.AutoFit();
                //                COMExcel.Range r11 = (COMExcel.Range)exSheet.Cells[i, 11];
                //                r11.Value2 = a.ThanhTien;
                //                r11.Columns.AutoFit();
                //                COMExcel.Range r12 = (COMExcel.Range)exSheet.Cells[i, 12];
                //                r12.Value2 = a.Xetnghiem;
                //                r12.Columns.AutoFit();
                //                COMExcel.Range r13 = (COMExcel.Range)exSheet.Cells[i, 13];
                //                r13.Value2 = a.CDHA;
                //                r13.Columns.AutoFit();
                //                COMExcel.Range r14 = (COMExcel.Range)exSheet.Cells[i, 14];
                //                r14.Value2 = a.Thuoc;
                //                r14.Columns.AutoFit();
                //                COMExcel.Range r15 = (COMExcel.Range)exSheet.Cells[i, 15];
                //                r15.Value2 = a.Mau;
                //                r15.Columns.AutoFit();
                //                COMExcel.Range r16 = (COMExcel.Range)exSheet.Cells[i, 16];
                //                r16.Value2 = a.TTPT;
                //                r16.Columns.AutoFit();
                //                COMExcel.Range r17 = (COMExcel.Range)exSheet.Cells[i, 17];
                //                r17.Value2 = a.VTYT;
                //                r17.Columns.AutoFit();
                //                COMExcel.Range r18 = (COMExcel.Range)exSheet.Cells[i, 18];
                //                r18.Value2 = 0;
                //                r18.Columns.AutoFit();
                //                COMExcel.Range r19 = (COMExcel.Range)exSheet.Cells[i, 19];
                //                r19.Value2 = a.DVKTC;
                //                r19.Columns.AutoFit();
                //                COMExcel.Range r20 = (COMExcel.Range)exSheet.Cells[i, 20];
                //                r20.Value2 = a.ThuocKTCG;
                //                r20.Columns.AutoFit();
                //                COMExcel.Range r21 = (COMExcel.Range)exSheet.Cells[i, 21];
                //                r21.Value2 = a.Congkham;
                //                r21.Columns.AutoFit();
                //                COMExcel.Range r22 = (COMExcel.Range)exSheet.Cells[i, 22];
                //                r22.Value2 = a.CPVanchuyen;
                //                r22.Columns.AutoFit();
                //                COMExcel.Range r23 = (COMExcel.Range)exSheet.Cells[i, 23];
                //                r23.Value2 = a.Nguoibenhchitra;
                //                r23.Columns.AutoFit();
                //                COMExcel.Range r24 = (COMExcel.Range)exSheet.Cells[i, 24];
                //                r24.Value2 = a.TongcongBHYT;
                //                r24.Columns.AutoFit();
                //                COMExcel.Range r25 = (COMExcel.Range)exSheet.Cells[i, 25];
                //                r25.Value2 = a.CPVanchuyen;
                //                r25.Columns.AutoFit();
                //                COMExcel.Range r26 = (COMExcel.Range)exSheet.Cells[i, 26];
                //                r26.Value2 = 1;
                //                r26.Columns.AutoFit();
                //                COMExcel.Range r27 = (COMExcel.Range)exSheet.Cells[i, 27];
                //                r27.Value2 = " ";
                //                r27.Columns.AutoFit();
                //                COMExcel.Range r28 = (COMExcel.Range)exSheet.Cells[i, 27];
                //                r28.Value2 = DungChung.Bien.MaBV;
                //                r28.Columns.AutoFit();
                //                COMExcel.Range r29 = (COMExcel.Range)exSheet.Cells[i, 29];
                //                r29.Value2 = a.MaKP;
                //                r29.Columns.AutoFit();
                //                COMExcel.Range r30 = (COMExcel.Range)exSheet.Cells[i, 30];
                //                if (a.NgayKham != null)
                //                {
                //                    r30.Value2 = a.NgayKham.ToString().Substring(3, 2);
                //                    r30.Columns.AutoFit();
                //                }
                //                COMExcel.Range r31 = (COMExcel.Range)exSheet.Cells[i, 31];

                //                if (a.NgayKham != null)
                //                {
                //                    r31.Value2 = a.NgayKham.ToString().Substring(6, 4);
                //                    r31.Columns.AutoFit();
                //                }
                //                COMExcel.Range r32 = (COMExcel.Range)exSheet.Cells[i, 32];
                //                if (a.HanBHTu != null)
                //                {
                //                    r32.Value2 = a.HanBHTu.ToString().Substring(0, 10);
                //                    r32.Columns.AutoFit();
                //                }
                //                COMExcel.Range r33 = (COMExcel.Range)exSheet.Cells[i, 33];
                //                if (a.HanBHDen != null)
                //                {
                //                    r33.Value2 = a.HanBHDen.ToString().Substring(0, 10);
                //                    r33.Columns.AutoFit();
                //                }

                //                COMExcel.Range r34 = (COMExcel.Range)exSheet.Cells[i, 34];
                //                r34.Value2 = a.DiaChi;
                //                r34.Columns.AutoFit();
                //                COMExcel.Range r35 = (COMExcel.Range)exSheet.Cells[i, 35];
                //                r35.Value2 = "";
                //                r35.Columns.AutoFit();
                //                COMExcel.Range r36 = (COMExcel.Range)exSheet.Cells[i, 36];
                //                r36.Value2 = 0;
                //                r36.Columns.AutoFit();
                //                COMExcel.Range r37 = (COMExcel.Range)exSheet.Cells[i, 37];
                //                r37.Value2 = "";
                //                r37.Columns.AutoFit();
                //                COMExcel.Range r38 = (COMExcel.Range)exSheet.Cells[i, 38];
                //                r38.Value2 = 0;
                //                r38.Columns.AutoFit();
                //                COMExcel.Range r39 = (COMExcel.Range)exSheet.Cells[i, 39];
                //                r39.Value2 = 0;
                //                r39.Columns.AutoFit();
                //                COMExcel.Range r40 = (COMExcel.Range)exSheet.Cells[i, 40];
                //                r40.Value2 = "NOI";
                //                r40.Columns.AutoFit();
                //                COMExcel.Range r41 = (COMExcel.Range)exSheet.Cells[i, 41];
                //                r41.Value2 = "CSKCB";
                //                r41.Columns.AutoFit();
                //                COMExcel.Range r42 = (COMExcel.Range)exSheet.Cells[i, 42];
                //                r42.Value2 = a.MaBNhan;
                //                r42.Columns.AutoFit();
                //                COMExcel.Range r43 = (COMExcel.Range)exSheet.Cells[i, 43];
                //                r43.Value2 = a.MaKP;
                //                r43.Columns.AutoFit();
                //                COMExcel.Range r44 = (COMExcel.Range)exSheet.Cells[i, 44];
                //                r44.Value2 = "";
                //                r44.Columns.AutoFit();

                //            }
                //            exApp.Visible = true;//Ẩn hiện chương trình
                //            exQLBV.SaveAs("C:\\Bieu11_BHYT.xls", COMExcel.XlFileFormat.xlWorkbookNormal,
                //                            null, null, false, false,
                //                            COMExcel.XlSaveAsAccessMode.xlExclusive,
                //                            false, false, false, false, false);
                //            //exQLBV.Close(false, false, false);
                //            //exApp.Quit(); // thoát ứng dụng
                //            System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV);
                //            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                //        }
                //        #endregion
                //    }
                //}
                //else
                //    MessageBox.Show("Không có dữ liệu");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_10ct_Load(object sender, EventArgs e)
        {
            lupTuNgay.Focus();
            lupTuNgay.DateTime = System.DateTime.Now;
            lupDenNgay.DateTime = System.DateTime.Now;
        }
    }
}