using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_KcBenhTatNL_TK04 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_KcBenhTatNL_TK04()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            colKhoaPhong.DataBindings.Add("Text", DataSource, "KhoaPhong");
            colTenICD.DataBindings.Add("Text", DataSource, "TenICD");
            txtMaICD.DataBindings.Add("Text", DataSource, "MaICD");
            colTongSo.DataBindings.Add("Text", DataSource, "TongSo");
            colTongSoTong.DataBindings.Add("Text", DataSource, "TongSo");
            colCanBo.DataBindings.Add("Text", DataSource, "CanBo");
            colCanBoTong.DataBindings.Add("Text", DataSource, "CanBo");
            colNhanDan.DataBindings.Add("Text", DataSource, "NhanDan");
            colNhanDanTong.DataBindings.Add("Text", DataSource, "NhanDan");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void colTongSo_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //string _maicd = "";
            //if (GetCurrentColumnValue("MaICD") != null)
            //    _maicd = GetCurrentColumnValue("MaICD").ToString();
            //var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 16)
            //           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
            //           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
            //           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
            //           group new { bn, bnkb } by new {icd.MaICD,bnkb.MaBNhan} into kq
            //           select new
            //           {
            //              MaICD = kq.Key.MaICD,
            //              TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

            //           }).ToList();
            //if (qbt.Count > 0)
            //{
            //   colTongSo.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();
            
            //}
        }

        private void colTongSoTong_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
          
            //var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 16)
            //           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
            //           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
            //           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
            //           group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
            //           select new
            //           {

            //                TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

            //           }).ToList();
            //if (qbt.Count > 0)
            //{
            //    colTongSoTong.Text = qbt.Sum(p => p.TongSo).ToString();

            //}
        }

        private void colCanBo_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //string _maicd = "";
            //if (GetCurrentColumnValue("MaICD") != null)
            //    _maicd = GetCurrentColumnValue("MaICD").ToString();
            //var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 16).Where(p=>p.DTuong.Contains("BHYT"))
            //           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
            //           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
            //           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
            //           group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
            //           select new
            //           {

            //               MaICD = kq.Key.MaICD,
            //               TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

            //           }).ToList();
            //if (qbt.Count > 0)
            //{
            //    colCanBo.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();

            //}
        }

        private void colCanBoTong_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            
            //var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 16).Where(p => p.DTuong.Contains("BHYT"))
            //           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
            //           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
            //           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
            //           group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
            //           select new
            //           {
            //              TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

            //           }).ToList();
            //if (qbt.Count > 0)
            //{
            //    colCanBoTong.Text = qbt.Sum(p => p.TongSo).ToString();

            //}
        }

        private void colNhanDan_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //string _maicd = "";
            //if (GetCurrentColumnValue("MaICD") != null)
            //    _maicd = GetCurrentColumnValue("MaICD").ToString();
            //var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 16).Where(p => p.DTuong.Contains("Dịch vụ"))
            //           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
            //           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
            //           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
            //           group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
            //           select new
            //           {

            //               MaICD = kq.Key.MaICD,
            //               TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

            //           }).ToList();
            //if (qbt.Count > 0)
            //{
            //    colNhanDan.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();

            //}
        }

        private void colNhanDanTong_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
             
            //var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 16).Where(p => p.DTuong.Contains("Dịch vụ"))
            //           join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
            //           join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
            //           where (bn.NNhap >= tungay && bn.NNhap <= denngay)
            //           group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
            //           select new
            //           {

            //                TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

            //           }).ToList();
            //if (qbt.Count > 0)
            //{
            //    colNhanDanTong.Text = qbt.Sum(p => p.TongSo).ToString();

            //}
        }
    }
}
