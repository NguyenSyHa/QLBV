using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_DsNopVP_TK01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DsNopVP_TK01()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities dataContext = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        public void BindingData()
        {
            txtNgay.DataBindings.Add("Text", DataSource, "NgayThang");
            colHoTen.DataBindings.Add("Text", DataSource, "HoTen");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colPhauThuat.DataBindings.Add("Text", DataSource, "PhauThuat").FormatString = DungChung.Bien.FormatString[1];
            colPhauThuatT.DataBindings.Add("Text", DataSource, "PhauThuat").FormatString = DungChung.Bien.FormatString[1];
            colThuThuat.DataBindings.Add("Text", DataSource, "ThuThuat").FormatString = DungChung.Bien.FormatString[1];
            colThuThuatT.DataBindings.Add("Text", DataSource, "ThuThuat").FormatString = DungChung.Bien.FormatString[1];
            colXQuang.DataBindings.Add("Text", DataSource, "XQuang").FormatString = DungChung.Bien.FormatString[1];
            colXQuangT.DataBindings.Add("Text", DataSource, "XQuang").FormatString = DungChung.Bien.FormatString[1];
            colTPDN.DataBindings.Add("Text", DataSource, "TPDN").FormatString = DungChung.Bien.FormatString[1];
            colTPDNT.DataBindings.Add("Text", DataSource, "TPDN").FormatString = DungChung.Bien.FormatString[1];
            colSieuAm.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            colSieuAmT.DataBindings.Add("Text", DataSource, "SieuAm").FormatString = DungChung.Bien.FormatString[1];
            colXetNghiem.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colXetNghiemT.DataBindings.Add("Text", DataSource, "XetNghiem").FormatString = DungChung.Bien.FormatString[1];
            colNgayGiuong.DataBindings.Add("Text", DataSource, "NgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colNgayGiuongT.DataBindings.Add("Text", DataSource, "NgayGiuong").FormatString = DungChung.Bien.FormatString[1];
            colCongKham.DataBindings.Add("Text", DataSource, "CongKham").FormatString = DungChung.Bien.FormatString[1];
            colCongKhamT.DataBindings.Add("Text", DataSource, "CongKham").FormatString = DungChung.Bien.FormatString[1];
            colTong.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
            colTongT.DataBindings.Add("Text", DataSource, "TongTien").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void colKhoaPhong_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("Khoa") != null && GetCurrentColumnValue("Khoa").ToString() != "")
            {
                var q = (from kp in dataContext.KPhongs.Where(p => p.MaKP== Convert.ToInt32( Khoa))
                         select new { kp.TenKP }).ToList();
                if (q.Count > 0)
                {
                    colKhoaPhong.Text = q.First().TenKP;
                }

            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            colKeToan.Text = DungChung.Bien.KeToanVP;
            colTTDV.Text = DungChung.Bien.GiamDoc;
        }

        private void colNgaythang_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("NgayThang") != null && GetCurrentColumnValue("NgayThang").ToString().Length >= 10)
            {
                colNgaythang.Text = txtNgay.Text.Substring(0, 10);
            }
        }



        private void txtsotien_BeforePrint(object sender, CancelEventArgs e)
        {
             Double st = Convert.ToDouble(TongTien.Value);
            st = Math.Round(st, 0);
            txtsotien.Text = DungChung.Ham.DocTienBangChu(st, " đồng");
        }
      }
    
}
