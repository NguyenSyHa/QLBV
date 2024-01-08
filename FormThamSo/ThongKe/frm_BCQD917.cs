using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using COMExcel = Microsoft.Office.Interop.Excel;


namespace QLBV.FormThamSo
{
    public partial class frm_BCQD917 : DevExpress.XtraEditors.XtraForm
    {
        public frm_BCQD917()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime tungay = DateTime.Now;
            DateTime denngay = DateTime.Now;
            tungay = DungChung.Ham.NgayTu(lupNgaytu.DateTime);
            denngay = DungChung.Ham.NgayDen(lupngayden.DateTime);
            int iddtbn = 100; int trongBH = 0;

            trongBH = rdTrongBH.SelectedIndex;

            var qvp = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                       join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT").Where(p=> (p.NoiTru == 1 && ckDTnt.Checked) || (p.NoiTru == 0 && p.DTNT == true && ckDTngt.Checked) || (p.NoiTru == 0 && p.DTNT == false && ckKBNgt.Checked)) on vp.MaBNhan equals bn.MaBNhan
                       join vpct in data.VienPhicts.Where(p=>p.TrongBH == 3 || p.TrongBH == trongBH) on vp.idVPhi equals vpct.idVPhi
                       select new { bn.MaBNhan, bn.TenBNhan, bn.NoiTru, bn.GTinh, bn.NgaySinh, bn.MaCS, bn.ThangSinh, bn.NamSinh, bn.Tuoi, bn.Tuyen, bn.MaKCB, bn.SThe, bn.SoDK, bn.MucHuong, bn.KhuVuc, bn.HanBHTu, bn.HanBHDen, bn.DTNT, bn.DChi, bn.CapCuu, vp.NgayTT, vpct.MaKP, vpct.MaDV, vpct.DonGia, vpct.DonVi, vpct.SoLuong, vpct.TrongBH, vpct.TyLeBHTT, vpct.TyLeTT, vpct.TienBH, vpct.TienBN, vpct.ThanhTien }).ToList();
            var qdv = (from dv in data.DichVus
                       join tn in data.TieuNhomDVs on dv.IdTieuNhom equals tn.IdTieuNhom
                       join n in data.NhomDVs on tn.IDNhom equals n.IDNhom
                       select new { n.IDNhom, n.TenNhomCT, tn.TenRG, dv.TenDV, dv.TenHC, dv.PLoai, dv.MaDV, dv.MaQD, dv.MaTam, dv.MaDuongDung, dv.DuongD, dv.HamLuong, dv.NuocSX }).ToList();
            var qttbx = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                         join bn in data.BenhNhans.Where(p => p.DTuong == "BHYT") on vp.MaBNhan equals bn.MaBNhan
                         join rv in data.RaViens on vp.MaBNhan equals rv.MaBNhan
                         join ttbx in data.TTboXungs on rv.MaBNhan equals ttbx.MaBNhan into kq
                         from kq1 in kq.DefaultIfEmpty()
                         join vv in data.VaoViens on rv.MaBNhan equals vv.MaBNhan into kqvv
                         from kq2 in kqvv.DefaultIfEmpty()
                         select new
                         {
                             rv.MaBNhan,
                             rv.NgayRa,
                             rv.ChanDoan,
                             rv.ChanDoanCV,
                             rv.IdRaVien,
                             rv.KetQua,
                             rv.MaKP,
                             rv.LoiDan,
                             rv.LyDoC,
                             rv.MaBVC,
                             rv.MaCB,
                             rv.MaICD,
                             rv.PPDTr,
                             rv.SoNgaydt,
                             rv.Status,
                             NThan = kq1 == null ? "" : kq1.NThan,
                             NgoaiKieu = kq1 == null ? "" : kq1.NgoaiKieu,
                             MaTinh = kq1 == null ? "" : kq1.MaTinh,
                             DThoai = kq1 == null ? "" : kq1.DThoai,
                             DThoaiNT = kq1 == null ? "" : kq1.DThoaiNT,
                             SoVV = kq2 == null ? "" : kq2.SoVV,
                             NgayVao = kq2 == null ? null : kq2.NgayVao,
                             MaKPVV = kq2 == null ? 0 : kq2.MaKP,
                             LyDovv = kq2 == null ? "" : kq2.LyDo,
                             ChanDoanvv = kq2 == null ? "" : kq2.ChanDoan,

                         }).ToList();

            var qdt0 = (from vp in data.VienPhis.Where(p => p.NgayTT >= tungay && p.NgayTT <= denngay)
                       join dt in data.DThuocs on vp.MaBNhan equals dt.MaBNhan
                       join dtct in data.DThuoccts on dt.IDDon equals dtct.IDDon
                       
                       group new { vp, dt, dtct } by new { vp.MaBNhan,  dtct.MaDV } into kq
                       select new
                       {
                          MaBNhan= kq.Key.MaBNhan??0,
                           IDDon = kq.Max(p=>p.dt.IDDon),                      
                           kq.Key.MaDV
                       }).ToList();

            var qdt1 = (from dt in data.DThuocs 
                       join cb in data.CanBoes on dt.MaCB equals cb.MaCB into kq from kq1 in kq.DefaultIfEmpty()
                       select new
                       {
                           dt.MaBNhan,
                          dt.IDDon,
                          dt.NgayKe,
                           MaCCHN = kq1==null ? "" : kq1.MaCCHN,                         
                       }).ToList();
            var qdt = (from dth in qdt0
                       join cb in qdt1 on dth.IDDon equals cb.IDDon
                       select new
                       {
                           dth.MaBNhan,
                           cb.NgayKe,
                           cb.MaCCHN,
                           dth.MaDV
                       }).ToList();

            //var qdt = (from dth in qdt0
            //           join dt in data.DThuocs on dth.IDDon equals dt.IDDon   
            //           join cb in data.CanBoes on dt.MaCB equals cb.MaCB
            //           select new
            //           {
            //              dth.MaBNhan,
            //              dt.NgayKe ,
            //              cb.MaCCHN,
            //              dth.MaDV
            //           }).ToList();

          
            var qall = (from vp in qvp
                        join dv in qdv on vp.MaDV equals dv.MaDV
                        join tt in qttbx on vp.MaBNhan equals tt.MaBNhan
                        join kp in data.KPhongs on vp.MaKP equals kp.MaKP into kq from kq1 in kq.DefaultIfEmpty()
                        join dt in qdt on new { vp.MaBNhan,vp.MaDV } equals new { MaBNhan = dt.MaBNhan, dt.MaDV }
                        select new
                        {
                            vp.MaBNhan,
                            vp.TenBNhan,
                            vp.MaCS,
                            vp.NoiTru,
                            vp.GTinh,
                            vp.NgaySinh,
                            vp.ThangSinh,
                            vp.NamSinh,
                            vp.Tuoi,
                            vp.Tuyen,
                            vp.MaKCB,
                            vp.SThe,
                            vp.SoDK,
                            vp.MucHuong,
                            vp.KhuVuc,
                            vp.HanBHTu,
                            vp.HanBHDen,
                            vp.DTNT,
                            vp.DChi,
                            vp.CapCuu,
                            vp.NgayTT,
                            vp.MaKP,
                            MaKPQD = kq1 == null ? "" : kq1.MaQD,
                            vp.DonGia,
                            vp.DonVi,
                            vp.SoLuong,
                            vp.TrongBH,
                            vp.TyLeBHTT,
                            vp.TienBH,
                            vp.TienBN,
                            vp.ThanhTien,
                            vp.TyLeTT,
                            dv.IDNhom,
                            dv.TenNhomCT,
                            dv.TenRG,
                            dv.TenDV,
                            dv.TenHC,
                            dv.PLoai,
                            dv.MaDV,
                            dv.MaQD,
                            dv.MaTam,
                            dv.MaDuongDung,
                            dv.DuongD,
                            dv.HamLuong,
                            dv.NuocSX,
                            tt.NgayRa,
                            tt.ChanDoan,
                            tt.ChanDoanCV,
                            tt.IdRaVien,
                            tt.KetQua,
                            dt.NgayKe,
                            dt.MaCCHN,
                            MaKPrv = tt.MaKP,
                            tt.LoiDan,
                            tt.LyDoC,
                            tt.MaBVC,
                            tt.MaCB,
                            tt.MaICD,
                            tt.PPDTr,
                            tt.SoNgaydt,
                            tt.Status,
                            MaICD1 = getICD(tt.MaICD, 1),
                            MaICD2 = getICD(tt.MaICD, 2),
                            TenBenh = getICD(tt.ChanDoan, 1),
                            TenBenhKhac = getICD(tt.ChanDoan, 2),
                            NThan = tt.NThan,
                            NgoaiKieu = tt.NgoaiKieu,
                            MaTinh = tt.MaTinh,
                            DThoai = tt.DThoai,
                            DThoaiNT = tt.DThoaiNT,
                            SoVV = tt.SoVV,
                            NgayVao = tt.NgayVao,
                            MaKPVV = tt.MaKP,
                            LyDovv = tt.LyDovv,
                            ChanDoanvv = tt.ChanDoan
                        }).OrderBy(p => p.MaBNhan).ToList();


           
            #region xuất file 1
            var q1 = (from a in qall
                      join kp in data.KPhongs on a.MaKPrv equals kp.MaKP
                      group a by new
                      {
                          a.MaBNhan,
                          a.TenBNhan,
                          a.MaCS,
                          a.NoiTru,
                          a.GTinh,
                          a.NgaySinh,
                          a.ThangSinh,
                          a.NamSinh,
                          a.Tuoi,
                          a.Tuyen,
                          a.MaKCB,
                          a.SThe,
                          a.SoDK,
                          a.MucHuong,
                          a.KhuVuc,
                          a.HanBHTu,
                          a.HanBHDen,
                          a.DTNT,
                          a.DChi,
                          a.CapCuu,
                          a.NgayTT,
                          a.NgayRa,
                          a.ChanDoan,
                          a.KetQua,
                          a.MaKPrv,
                          a.MaBVC,
                          a.MaICD,
                          a.SoNgaydt,
                          a.Status,
                          a.NgayVao,
                          a.MaKPVV,
                          MaQDRv = kp.MaQD,
                          a.MaICD1,
                          a.MaICD2,
                          a.TenBenh,
                          a.TenBenhKhac,

                      } into kq
                      select new
                      {
                          kq.Key.MaBNhan,
                          kq.Key.TenBNhan,
                          kq.Key.MaCS,
                          kq.Key.NoiTru,
                          kq.Key.GTinh,
                          kq.Key.NgaySinh,
                          kq.Key.ThangSinh,
                          kq.Key.NamSinh,
                          kq.Key.Tuoi,
                          kq.Key.Tuyen,
                          kq.Key.MaKCB,
                          kq.Key.SThe,
                          kq.Key.SoDK,
                          kq.Key.MucHuong,
                          kq.Key.KhuVuc,
                          HanBHTu = kq.Key.HanBHTu == null ? "" : kq.Key.HanBHTu.Value.ToString("dd/MM/yyyy"),
                          HanBHDen = kq.Key.HanBHDen == null ? "" : kq.Key.HanBHDen.Value.ToString("dd/MM/yyyy"),
                          kq.Key.DTNT,
                          kq.Key.DChi,
                          kq.Key.CapCuu,
                          kq.Key.NgayTT,
                          kq.Key.NgayRa,
                      
                          kq.Key.MaICD1,
                          kq.Key.MaICD2,
                          kq.Key.TenBenh,
                          kq.Key.TenBenhKhac,
                          ma_lydo_vvien = kq.Key.CapCuu == 1 ? 2 : (kq.Key.Tuyen == 1 ? 1 : 3),
                          ma_noi_chuyen = kq.Key.MaBVC,
                          ma_tai_nan = 0,
                          ngay_vao =  kq.Key.NgayVao == null ? "" : kq.Key.NgayVao.Value.ToString("yyyyMMdd"),
                          ngay_ra = kq.Key.NgayRa == null ? "" : kq.Key.NgayRa.Value.ToString("yyyyMMdd"),
                          tinh_trang_rv = (kq.Key.Status == null || kq.Key.Status == 2) ? 1 : (kq.Key.Status == 1 ? 2 : 3),
                          so_ngay_dtri = kq.Key.SoNgaydt,
                          ngay_ttoan = kq.Key.NgayTT == null ? "" : kq.Key.NgayTT.Value.ToString("yyyyMMdd"),
                          muc_huong = kq != null ? kq.First().TyLeBHTT : 100,
                          t_thuoc = kq.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7).Sum(p => p.ThanhTien),
                          t_vtyt = kq.Where(p => p.IDNhom == 10 || p.IDNhom == 11).Sum(p => p.ThanhTien),
                          t_tongchi = kq.Sum(p => p.ThanhTien),
                          t_bntt = kq.Sum(p => p.TienBN),
                          t_bhtt = kq.Sum(p => p.TienBH),
                          t_ngoaids = kq.Where(p => p.TenNhomCT == "Vận chuyển").Sum(p => p.ThanhTien),
                          ma_loai_kcb = kq.Key.NoiTru == 1 ? 3 : (kq.Key.DTNT == true ? 2 : 1),
                          ma_khoa = kq.Key.MaQDRv, //getMaKPQD(kq.Key.MaKPrv),
                          ma_cskcb = kq.Key.MaKCB,
                          ma_khuvuc = kq.Key.KhuVuc,
                          ket_qua_dtri = (kq.Key.KetQua == null || kq.Key.KetQua == "Đỡ|Giảm") ? 2 : (kq.Key.KetQua == "Khỏi" ? 1 : (kq.Key.KetQua == "Không T.đổi" ? 3 : (kq.Key.KetQua == "Tử vong" ? 5 : 4)))
                      }).OrderBy(p => p.MaBNhan).ToList();
            COMExcel.Application exApp1 = new COMExcel.Application();
            COMExcel.Workbook exQLBV1 = exApp1.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet1 = (COMExcel.Worksheet)exQLBV1.Worksheets[1];
            string[] _arr = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude = { "ma_lk", "stt", "ma_bn", "ho_ten", "ngay_sinh", "gioi_tinh", "dia_chi", "ma_the", "ma_dkbd", "gt_the_tu", "gt_the_den", "ten_benh", "ma_benh", "ma_benhkhac", "ma_lydo_vvien", "ma_noi_chuyen", "ma_tai_nan", "ngay_vao", "ngay_ra", "so_ngay_dtri", "ket_qua_dtri", "tinh_trang_rv", "ngay_ttoan", "muc_huong", "t_thuoc", "t_vtyt", "t_tongchi", "t_bntt", "t_bhtt", "t_nguonkhac", "t_ngoaids", "ma_loai_kcb", "ma_khoa", "ma_cskcb", "ma_khuvuc", "ma_pttt_qt", "can_nang" };
            string _filePath = "D:\\excel1.xlsx";
            int[] _arrWidth = new int[] { };
            DungChung.Bien.MangHaiChieu = new Object[q1.Count + 1, 37];
            for (int i = 0; i < 37; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude[i];
            }
            int num = 1;
            foreach (var r in q1)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = r.MaBNhan;
                DungChung.Bien.MangHaiChieu[num, 1] = num;
                DungChung.Bien.MangHaiChieu[num, 2] = r.MaBNhan;
                DungChung.Bien.MangHaiChieu[num, 3] = r.TenBNhan;
                DungChung.Bien.MangHaiChieu[num, 4] = r.NamSinh;
                DungChung.Bien.MangHaiChieu[num, 5] = r.GTinh == 0 ? 2 : r.GTinh;
                DungChung.Bien.MangHaiChieu[num, 6] = r.DChi;
                DungChung.Bien.MangHaiChieu[num, 7] = r.SThe;
                DungChung.Bien.MangHaiChieu[num, 8] = r.MaCS;
                DungChung.Bien.MangHaiChieu[num, 9] = r.HanBHTu;

                DungChung.Bien.MangHaiChieu[num, 10] = r.HanBHDen;
                DungChung.Bien.MangHaiChieu[num, 11] = r.TenBenh;
                DungChung.Bien.MangHaiChieu[num, 12] = r.MaICD1;
                DungChung.Bien.MangHaiChieu[num, 13] = r.MaICD2;
                DungChung.Bien.MangHaiChieu[num, 14] = r.ma_lydo_vvien;
                DungChung.Bien.MangHaiChieu[num, 15] = r.ma_noi_chuyen;
                DungChung.Bien.MangHaiChieu[num, 16] = r.ma_tai_nan;
                DungChung.Bien.MangHaiChieu[num, 17] = r.ngay_vao;
                DungChung.Bien.MangHaiChieu[num, 18] = r.ngay_ra;
                DungChung.Bien.MangHaiChieu[num, 19] = r.so_ngay_dtri;

                DungChung.Bien.MangHaiChieu[num, 20] = r.ket_qua_dtri;
                DungChung.Bien.MangHaiChieu[num, 21] = r.tinh_trang_rv;
                DungChung.Bien.MangHaiChieu[num, 22] = r.ngay_ttoan;
                DungChung.Bien.MangHaiChieu[num, 23] = r.muc_huong;
                DungChung.Bien.MangHaiChieu[num, 24] = r.t_thuoc;
                DungChung.Bien.MangHaiChieu[num, 25] = r.t_vtyt;
                DungChung.Bien.MangHaiChieu[num, 26] = r.t_tongchi;
                DungChung.Bien.MangHaiChieu[num, 27] = r.t_bntt;
                DungChung.Bien.MangHaiChieu[num, 28] = r.t_bhtt;
                DungChung.Bien.MangHaiChieu[num, 29] = 0;

                DungChung.Bien.MangHaiChieu[num, 30] = r.t_ngoaids;
                DungChung.Bien.MangHaiChieu[num, 31] = r.ma_loai_kcb;
                DungChung.Bien.MangHaiChieu[num, 32] = r.ma_khoa;
                DungChung.Bien.MangHaiChieu[num, 33] = r.ma_cskcb;
                DungChung.Bien.MangHaiChieu[num, 34] = r.ma_khuvuc;
                DungChung.Bien.MangHaiChieu[num, 35] = "";
                DungChung.Bien.MangHaiChieu[num, 36] = "";
                num++;
            }
        //    QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr, _arrWidth, "123", _filePath, true);
           
            exSheet1.Range[exSheet1.Cells[1, 33], exSheet1.Cells[num, 34]].NumberFormat = "@";// ngày ra   
            //-------------------------------------------------------------------------------------------
           
           
            exSheet1.Range[exSheet1.Cells[1, 1], exSheet1.Cells[num , 37]].Value = DungChung.Bien.MangHaiChieu;
            exApp1.Visible = true;
            try
            {

                exQLBV1.SaveAs(_filePath, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);
              
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp1);
            }

            #endregion
            #region xuất excel2
            var q2 = qall.Where(p => p.IDNhom == 4 || p.IDNhom == 5 || p.IDNhom == 6 || p.IDNhom == 7).OrderBy(p => p.MaBNhan).ToList();
            string[] _arr2 = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude2 = { "ma_lk", "stt", "ma_thuoc", "ma_nhom", "ten_thuoc", "don_vi_tinh", "ham_luong", "duong_dung", "lieu_dung", "so_dang_ky", "so_luong", "don_gia", "tyle_tt", "thanh_tien", "ma_khoa", "ma_bac_si", "ma_benh", "ngay_yl", "ma_pttt", "ma_thuoc_bv" };
            string _filePath2 = "D:\\excel2.xlsx";
            int[] _arrWidth2 = new int[] { };
            DungChung.Bien.MangHaiChieu = new Object[q2.Count + 1, 20];
            for (int i = 0; i < 20; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude2[i];
            }
            num = 1;
            foreach (var r in q2)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = r.MaBNhan;
                DungChung.Bien.MangHaiChieu[num, 1] = num;
                DungChung.Bien.MangHaiChieu[num, 2] = r.MaQD;
                DungChung.Bien.MangHaiChieu[num, 3] = r.IDNhom;
                DungChung.Bien.MangHaiChieu[num, 4] = r.TenDV;
                DungChung.Bien.MangHaiChieu[num, 5] = r.DonVi;
                DungChung.Bien.MangHaiChieu[num, 6] = r.HamLuong;
                DungChung.Bien.MangHaiChieu[num, 7] = r.DuongD;
                DungChung.Bien.MangHaiChieu[num, 8] = "";
                DungChung.Bien.MangHaiChieu[num, 9] = r.SoDK;

                DungChung.Bien.MangHaiChieu[num, 10] = r.SoLuong;
                DungChung.Bien.MangHaiChieu[num, 11] = r.DonGia;
                DungChung.Bien.MangHaiChieu[num, 12] = r.TyLeTT;
                DungChung.Bien.MangHaiChieu[num, 13] = r.ThanhTien;
                DungChung.Bien.MangHaiChieu[num, 14] = r.MaKPQD;
                //var qbs = qdt.Where(p => p.MaBNhan == r.MaBNhan && p.MaKP == r.MaKP && p.MaDV == r.MaDV).FirstOrDefault();
                //if (qbs != null)
                //{
                //    DungChung.Bien.MangHaiChieu[num, 15] = qbs.MaCCHN;
                //    if (qbs.NgayKe != null)
                //        DungChung.Bien.MangHaiChieu[num, 17] = qbs.NgayKe.Value.ToString("dd/MM/yyyy");
                //}
                DungChung.Bien.MangHaiChieu[num, 15] = r.MaCCHN;
                DungChung.Bien.MangHaiChieu[num, 17] = r.NgayKe == null ? "" : r.NgayKe.Value.ToString("dd/MM/yyyy");
                DungChung.Bien.MangHaiChieu[num, 16] = r.MaICD1;
                DungChung.Bien.MangHaiChieu[num, 18] = 1;
                DungChung.Bien.MangHaiChieu[num, 19] = r.MaTam;
                num++;
            }
            //QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr2, _arrWidth2, "123", _filePath2, true);
            COMExcel.Application exApp2 = new COMExcel.Application();
            COMExcel.Workbook exQLBV2 = exApp2.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet2 = (COMExcel.Worksheet)exQLBV2.Worksheets[1];
            exSheet2.Range[exSheet2.Cells[1, 18], exSheet2.Cells[num, 18]].NumberFormat = "@";// ngày ra   
            //-------------------------------------------------------------------------------------------


            exSheet2.Range[exSheet2.Cells[1, 1], exSheet2.Cells[num, 20]].Value = DungChung.Bien.MangHaiChieu;
            exApp2.Visible = true;
            try
            {

                exQLBV2.SaveAs(_filePath, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV2);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp2);
            }
            #endregion

            #region xuất excel3
            var q3 = qall.Where(p => p.PLoai == 2 || p.PLoai == 3 || (p.PLoai == 1 && (p.IDNhom == 10 || p.IDNhom == 11))).OrderBy(p => p.MaBNhan).ToList();
            string[] _arr3 = new string[] { "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@", "@" };
            string[] _tieude3 = { "ma_dvkt_bv", "ma_lk", "stt", "ma_dich_vu", "ma_vat_tu", "ma_nhom", "ten_dich_vu", "don_vi_tinh", "so_luong", "don_gia", "don_gia_kt", "tyle_tt", "thanh_tien", "ma_khoa", "ma_bac_si", "ma_benh", "ngay_yl", "ngay_kq", "ma_pttt", "ngay_kt" };
            string _filePath3 = "D:\\excel3.xlsx";
            int[] _arrWidth3 = new int[] { };
            DungChung.Bien.MangHaiChieu = new Object[q3.Count + 1, 20];
            for (int i = 0; i < 20; i++)
            {
                DungChung.Bien.MangHaiChieu[0, i] = _tieude3[i];
            }
            num = 1;
            foreach (var r in q3)
            {
                DungChung.Bien.MangHaiChieu[num, 0] = r.MaDV;
                DungChung.Bien.MangHaiChieu[num, 1] = r.MaBNhan;
                DungChung.Bien.MangHaiChieu[num, 2] = num;
                DungChung.Bien.MangHaiChieu[num, 3] = r.PLoai == 2 ? r.MaQD : "";
                DungChung.Bien.MangHaiChieu[num, 4] = r.PLoai != 2 ? r.MaQD : "";
                DungChung.Bien.MangHaiChieu[num, 5] = r.IDNhom;
                DungChung.Bien.MangHaiChieu[num, 6] = r.TenDV;
                DungChung.Bien.MangHaiChieu[num, 7] = r.DonVi;
                DungChung.Bien.MangHaiChieu[num, 8] = r.SoLuong;
                DungChung.Bien.MangHaiChieu[num, 9] = r.DonGia;

                DungChung.Bien.MangHaiChieu[num, 10] = 0;
                DungChung.Bien.MangHaiChieu[num, 11] = r.TyLeTT;
                DungChung.Bien.MangHaiChieu[num, 12] = r.ThanhTien;
                DungChung.Bien.MangHaiChieu[num, 13] = r.MaKPQD; //getMaKPQD(r.MaKP);
                //var qbs = qdt.Where(p => p.MaBNhan == r.MaBNhan && p.MaKP == r.MaKP && p.MaDV == r.MaDV).FirstOrDefault();
                //if (qbs != null)
                //{
                //    DungChung.Bien.MangHaiChieu[num, 14] = qbs.MaCCHN;
                //    if (qbs.NgayKe != null)
                //        DungChung.Bien.MangHaiChieu[num, 16] = qbs.NgayKe.Value.ToString("dd/MM/yyyy");
                //}
                DungChung.Bien.MangHaiChieu[num, 14] = r.MaCCHN;
                DungChung.Bien.MangHaiChieu[num, 16] = r.NgayKe == null ? "" : r.NgayKe.Value.ToString("dd/MM/yyyy");
                DungChung.Bien.MangHaiChieu[num, 15] = r.MaICD1;
                DungChung.Bien.MangHaiChieu[num, 17] = r.NgayRa.Value.ToString("dd/MM/yyyy");
                DungChung.Bien.MangHaiChieu[num, 18] = 1;
                DungChung.Bien.MangHaiChieu[num, 19] = r.NgayRa.Value.ToString("dd/MM/yyyy");
                num++;
            }
           // QLBV_Library.QLBV_Ham.xuatExcelArr(DungChung.Bien.MangHaiChieu, _arr3, _arrWidth3, "3", _filePath3, true);
            COMExcel.Application exApp3 = new COMExcel.Application();
            COMExcel.Workbook exQLBV3 = exApp3.Workbooks.Add(
                      COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet3 = (COMExcel.Worksheet)exQLBV3.Worksheets[1];
            exSheet3.Range[exSheet3.Cells[1, 17], exSheet3.Cells[num, 20]].NumberFormat = "@";
            exSheet3.Range[exSheet3.Cells[1, 18], exSheet3.Cells[num, 20]].NumberFormat = "@";
            exSheet3.Range[exSheet3.Cells[1, 20], exSheet3.Cells[num, 20]].NumberFormat = "@";
            //-------------------------------------------------------------------------------------------


            exSheet3.Range[exSheet3.Cells[1, 1], exSheet3.Cells[num, 20]].Value = DungChung.Bien.MangHaiChieu;
            exApp3.Visible = true;
            try
            {

                exQLBV3.SaveAs(_filePath, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exQLBV3);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp3);
            }
            #endregion
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="maICD">mã hoặc tên ICd trong bảng ra viện</param>
        /// <param name="stt">1: icd chính hoặc bệnh chính; 2: icd phụ hoặc bệnh phụ</param>
        /// <returns></returns>
        private string getICD(string maICD, int stt)
        {
            string[] arr;
            if (maICD != null)
            {
                arr = maICD.Split(';');
                if (stt == 1 && arr.Count() > 0)
                {
                    return arr[0];
                }
                else if (stt == 2 && arr.Count() > 1)
                {
                    return string.Join(",", arr.Skip(1));
                }
                else return "";

            }
            else return "";
        }
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        List<KPhong> _lkp = new List<KPhong>();
        private void frm_BCQD917_Load(object sender, EventArgs e)
        {
            lupNgaytu.DateTime = DateTime.Now;
            lupngayden.DateTime = DateTime.Now;
            _lkp = data.KPhongs.ToList();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}