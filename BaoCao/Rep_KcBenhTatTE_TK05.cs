using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_KcBenhTatTE_TK05 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_KcBenhTatTE_TK05()
        {
            InitializeComponent();
       
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
       
        public void BindingData()
        {
            colKhoaPhong.DataBindings.Add("Text", DataSource, "KhoaPhong");
            colTenICD.DataBindings.Add("Text", DataSource, "TenICD");
            txtMaICD.DataBindings.Add("Text", DataSource, "MaICD");

            //colTongSo.DataBindings.Add("Text", DataSource, "TongSo");
            //colTongSoTong.DataBindings.Add("Text", DataSource, "TongSo");

            //colNoiTru6.DataBindings.Add("Text", DataSource, "NoiTru6");
            //colNoiTru6Tong.DataBindings.Add("Text", DataSource, "NoiTru6");
            //colNgoaiTru6.DataBindings.Add("Text", DataSource, "NgoaiTru6");
            //colNgoaiTru6Tong.DataBindings.Add("Text", DataSource, "NgoaiTru6");

            //colNoiTru15.DataBindings.Add("Text", DataSource, "NoiTru15");
            //colNoiTru15Tong.DataBindings.Add("Text", DataSource, "NoiTru15");
            //colNgoaiTru15.DataBindings.Add("Text", DataSource, "NgoaiTru15");
            //colNgoaiTru15Tong.DataBindings.Add("Text", DataSource, "NgoaiTru15");

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }



        private void colTongSo_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            string _maicd = "";
            if (GetCurrentColumnValue("MaICD") != null)
                _maicd = GetCurrentColumnValue("MaICD").ToString();
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi < 15)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                           MaICD = kq.Key.MaICD,
                           TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colTongSo.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();

            }
        }

        private void colTongSoTong_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));

            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi < 15)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                           TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colTongSoTong.Text = qbt.Sum(p => p.TongSo).ToString();

            }
        }
        private void colNoiTru6_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            string _maicd = "";
            if (GetCurrentColumnValue("MaICD") != null)
                _maicd = GetCurrentColumnValue("MaICD").ToString();
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi <=6).Where(p=>p.NoiTru==1)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                           MaICD = kq.Key.MaICD,
                           TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNoiTru6.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();

            }
        }

        private void colNoiTru6Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
         
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi <= 6).Where(p => p.NoiTru == 1)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                            TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNoiTru6Tong.Text = qbt.Sum(p => p.TongSo).ToString();

            }
        }

        private void colNgoaiTru6_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            string _maicd = "";
            if (GetCurrentColumnValue("MaICD") != null)
                _maicd = GetCurrentColumnValue("MaICD").ToString();
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi <= 6).Where(p => p.NoiTru == 0)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                           MaICD = kq.Key.MaICD,
                           TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNgoaiTru6.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();

            }
        }

        private void colNgoaiTru6Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
          
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi <= 6).Where(p => p.NoiTru == 0)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                           MaICD = kq.Key.MaICD,
                           TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNgoaiTru6Tong.Text = qbt.Sum(p => p.TongSo).ToString();

            }
        }

        private void colNoiTru15_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            string _maicd = "";
            if (GetCurrentColumnValue("MaICD") != null)
                _maicd = GetCurrentColumnValue("MaICD").ToString();
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 6).Where(p=>p.Tuoi<15).Where(p => p.NoiTru == 1)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                           MaICD = kq.Key.MaICD,
                           TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNoiTru15.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();

            }
        }

        private void colNoiTru15Tong_BeforePrint(object sender, CancelEventArgs e)
        {

            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 6).Where(p => p.Tuoi < 15).Where(p => p.NoiTru == 1)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                            TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNoiTru15Tong.Text = qbt.Sum(p => p.TongSo).ToString();

            }
        }

        private void colNgoaiTru15_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            string _maicd = "";
            if (GetCurrentColumnValue("MaICD") != null)
                _maicd = GetCurrentColumnValue("MaICD").ToString();
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 6).Where(p => p.Tuoi < 15).Where(p => p.NoiTru == 0)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                           MaICD = kq.Key.MaICD,
                           TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNgoaiTru15.Text = qbt.Where(p => p.MaICD == _maicd).Sum(p => p.TongSo).ToString();

            }
        }

        private void colNgoaiTru15Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            DateTime tungay = System.DateTime.Now.Date;
            DateTime denngay = System.DateTime.Now.Date;
            tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            
            var qbt = (from bn in dataContext.BenhNhans.Where(p => p.Tuoi >= 6).Where(p => p.Tuoi < 15).Where(p => p.NoiTru == 0)
                       join bnkb in dataContext.BNKBs on bn.MaBNhan equals bnkb.MaBNhan
                       join icd in dataContext.ICD10 on bnkb.MaICD equals icd.MaICD
                       where (bn.NNhap >= tungay && bn.NNhap <= denngay)
                       group new { bn, bnkb } by new { icd.MaICD, bnkb.MaBNhan } into kq
                       select new
                       {

                            TongSo = kq.Select(p => p.bn.MaBNhan).Distinct().Count(),

                       }).ToList();
            if (qbt.Count > 0)
            {
                colNgoaiTru15Tong.Text = qbt.Sum(p => p.TongSo).ToString();

            }
        }
    }
}
