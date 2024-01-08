using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_HdKCBPhongKham_TK06 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_HdKCBPhongKham_TK06()
        {
            InitializeComponent();
        }
     //   QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            colPhongKham.DataBindings.Add("Text", DataSource, "TenCK");
            colTongKhamChung.DataBindings.Add("Text", DataSource, "TongKhamChung");
            colTongKhamChungT.DataBindings.Add("Text", DataSource, "TongKhamChung");
            colTongKhamBHYT.DataBindings.Add("Text", DataSource, "TongKhamBHYT");
            colTongKhamBHYTT.DataBindings.Add("Text", DataSource, "TongKhamBHYT");

            colCapCuu.DataBindings.Add("Text", DataSource, "CapCuu");
            colCapCuuT.DataBindings.Add("Text", DataSource, "CapCuu");
            colXetNghiem.DataBindings.Add("Text", DataSource, "XetNghiem");
            colXetNghiemT.DataBindings.Add("Text", DataSource, "XetNghiem");
            colXQuang.DataBindings.Add("Text", DataSource, "XQuang");
            colXQuangT.DataBindings.Add("Text", DataSource, "XQuang");
            colGioiThieu.DataBindings.Add("Text", DataSource, "GioiThieu");
            colGioiThieuT.DataBindings.Add("Text", DataSource, "GioiThieu");

            //colBHYT.DataBindings.Add("Text", DataSource, "TongKhamBHYT");
            //colHoNgheo.DataBindings.Add("Text", DataSource, "HoNgheo");
            //colNoiTru.DataBindings.Add("Text", DataSource, "NoiTru");
            //colNgoaiTru.DataBindings.Add("Text", DataSource, "NgoaiTru");
            //colTE.DataBindings.Add("Text", DataSource, "TE");
            //colNguoiLon.DataBindings.Add("Text", DataSource, "NguoiLon");
            //colDongY.DataBindings.Add("Text", DataSource, "DongY");
          }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colTruongKhoa.Text = DungChung.Bien.TruongKhoaLS;
        }

        private void colXetNghiemT_BeforePrint(object sender, CancelEventArgs e)
        {
   
        }

        private void colGioiThieu_BeforePrint(object sender, CancelEventArgs e)
        {
            //DateTime tungay = System.DateTime.Now.Date;
            //DateTime denngay = System.DateTime.Now.Date;
            //tungay = DungChung.Ham.NgayTu(Convert.ToDateTime(TuNgay.Value));
            //denngay = DungChung.Ham.NgayDen(Convert.ToDateTime(DenNgay.Value));
            //var q = (from bnkb in data.BNKBs
            //         where (bnkb.NgayKham >= tungay && bnkb.NgayKham <= denngay && bnkb.PhuongAn == 2)
            //         select new { 
            //             bnkb.MaBNhan }).ToList();
            //if (q.Count > 0)
            //{
            //    colGioiThieu.Text = q.Select(p => p.MaBNhan).Count().ToString();
            //}
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            string _chuyenkhoa = "";
            if (GetCurrentColumnValue("tenck") != null)
                _chuyenkhoa = GetCurrentColumnValue("tenck").ToString();
            if (string.IsNullOrEmpty(_chuyenkhoa))
                colPhongKham.Text = "Khám chung";
        }
    }
}
