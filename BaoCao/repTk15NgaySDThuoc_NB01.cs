using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repTk15NgaySDThuoc_NB01 : DevExpress.XtraReports.UI.XtraReport
    {
        public repTk15NgaySDThuoc_NB01()
        {
            InitializeComponent();
        }
        int makhoa = 0;
        string[] _songay=new string[50];
        public repTk15NgaySDThuoc_NB01(string[] ngay,int mk)
        {
            InitializeComponent();
            _songay = ngay;
            makhoa = mk;
        }
        
        
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            txtKhoa.DataBindings.Add("Text", DataSource, "MaKPnx");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colQuyCach.DataBindings.Add("Text", DataSource, "QuyCach");
            colTong.DataBindings.Add("Text", DataSource, "SoLuongT");
            colTongTong.DataBindings.Add("Text", DataSource, "SoLuongT");
           // col1Tong.DataBindings.Add("Text", DataSource, "SoLuongN");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string[] sngay = new string[60];
            for (int a = 0; a <= 50; a++)
            {
                sngay[a] = "01/01/2000";
            }
           
           
            for (int i = 0; i < _songay.Length;i++ )
            {
                sngay[i] = _songay[i];
            }
            if(sngay[0].Length>=8)
            col1.Text =DungChung.Ham.ConvertNgay(sngay[0]).Day.ToString();
            if (sngay[1].Length >= 8)
                col2.Text = DungChung.Ham.ConvertNgay(sngay[1]).Day.ToString();
            if (sngay[2].Length >= 8)
                col3.Text = DungChung.Ham.ConvertNgay(sngay[2]).Day.ToString();
            if (sngay[3].Length >= 8)
                col4.Text = DungChung.Ham.ConvertNgay(sngay[3]).Day.ToString();
            if (sngay[4].Length >= 8)
                col5.Text = DungChung.Ham.ConvertNgay(sngay[4]).Day.ToString();
            if (sngay[5].Length >= 8)
                col6.Text = DungChung.Ham.ConvertNgay(sngay[5]).Day.ToString();
            if (sngay[6].Length >= 8)
                col7.Text = DungChung.Ham.ConvertNgay(sngay[6]).Day.ToString();
            if (sngay[7].Length >= 8)
                col8.Text = DungChung.Ham.ConvertNgay(sngay[7]).Day.ToString();
            if (sngay[8].Length >= 8)
                col9.Text = DungChung.Ham.ConvertNgay(sngay[8]).Day.ToString();
            if (sngay[9].Length >= 8)
                col10.Text = DungChung.Ham.ConvertNgay(sngay[9]).Day.ToString();
            if (sngay[10].Length >= 8)
                col11.Text = DungChung.Ham.ConvertNgay(sngay[10]).Day.ToString();
            if (sngay[11].Length >= 8)
                col12.Text = DungChung.Ham.ConvertNgay(sngay[11]).Day.ToString();
            if (sngay[12].Length >= 8)
                col13.Text = DungChung.Ham.ConvertNgay(sngay[12]).Day.ToString();
            if (sngay[13].Length >= 8)
                col14.Text = DungChung.Ham.ConvertNgay(sngay[13]).Day.ToString();
            if (sngay[14].Length >= 8)
                col15.Text = DungChung.Ham.ConvertNgay(sngay[14]).Day.ToString();
            if (sngay[15].Length >= 8)
                col16.Text = DungChung.Ham.ConvertNgay(sngay[15]).Day.ToString();
            colCQCQ.Text = DungChung.Bien.TenCQCQ;
            colCQ.Text = DungChung.Bien.TenCQ;


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiThongKe.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colTKLamSang.Text = DungChung.Bien.TruongKhoaLS;
            if (DungChung.Bien.MaBV == "08204")
                colKDUoc.Text = "Kế toán dược".ToUpper();
        }
        QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //private void ngay()
        //{
        //    //ngaytu1 = GetCurrentColumnValue("ngaytu").ToString().Trim();
        //    //ngayden1 = ngayden.Value;
        //    //DateTime _ngayt =Convert.ToDateTime(TuNgay.Value);
        //    //DateTime _ngayd = Convert.ToDateTime(DenNgay.Value);
        //    int _ngayt = Convert.ToInt32(TuNgay.Value);
        //    int _ngayd = Convert.ToInt32(DenNgay.Value);

        //    for (int ngay = _ngayt; ngay <= _ngayd; ngay++)
        //    {
        //        //col1.Text = _ngayd;
        //    }
        //}

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
        private static string SL(int MaDV, int makp, DateTime ngay)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string soluong="";
            if (makp <=0)
            {
                var sl = (from nd in Data.NhapDs.Where(p => p.PLoai == 2).Where(p=>p.KieuDon==1)
                          where (nd.NgayNhap.Value.Day == ngay.Day && nd.NgayNhap.Value.Month == ngay.Month && nd.NgayNhap.Value.Year == ngay.Year)
                          join ndct in Data.NhapDcts.Where(p => p.MaDV==MaDV) on nd.IDNhap equals ndct.IDNhap
                          group new { nd, ndct } by new { ndct.MaDV, ndct.DonVi } into kq
                          select new { kq.Key.DonVi, kq.Key.MaDV, SoLuong = kq.Sum(p => p.ndct.SoLuongX) }).ToList();
                if (sl.Count > 0)
                    soluong = sl.First().SoLuong.ToString();
                return soluong;
            }
            else {
                var sl = (from nd in Data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1).Where(p => p.MaKPnx==makp)
                          where (nd.NgayNhap.Value.Day == ngay.Day && nd.NgayNhap.Value.Month == ngay.Month && nd.NgayNhap.Value.Year == ngay.Year)
                          join ndct in Data.NhapDcts.Where(p => p.MaDV==MaDV) on nd.IDNhap equals ndct.IDNhap
                          group new {ndct } by new { ndct.MaDV, ndct.DonVi } into kq
                          select new { kq.Key.DonVi, kq.Key.MaDV, SoLuong = kq.Sum(p => p.ndct.SoLuongX) }).ToList();
                if (sl.Count > 0)
                    soluong = sl.First().SoLuong.ToString();
                return soluong;
            }
        }
        private static string SLNgay(int makp, DateTime ngay)
        {
            QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string soluongngay = "";
            var sl = (from nd in Data.NhapDs.Where(p => p.PLoai == 2).Where(p => p.KieuDon == 1)
                      where (nd.MaKPnx==makp)
                      where (nd.NgayNhap.Value.Day == ngay.Day && nd.NgayNhap.Value.Month == ngay.Month && nd.NgayNhap.Value.Year == ngay.Year)
                      join ndct in Data.NhapDcts on nd.IDNhap equals ndct.IDNhap
                      group new { nd, ndct } by new { nd.NgayNhap.Value.Day, nd.NgayNhap.Value.Month } into kq
                      select new { SoLuongX=kq.Sum(p=>p.ndct.SoLuongX)}).ToList();
            if (sl.Count > 0)
                soluongngay = sl.Sum(p => p.SoLuongX).ToString();
            return soluongngay;
        }

        private void col1de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv=0;
            int _makp = 0;
            if(GetCurrentColumnValue("MaDV")!=null)
             _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32( GetCurrentColumnValue("MaKPnx"));
           col1de.Text= SL(_madv,_makp,DungChung.Ham.ConvertNgay(_songay[0]));
        }

        private void col2de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32( GetCurrentColumnValue("MaKPnx"));
            //System.Windows.Forms.MessageBox.Show(DungChung.Ham.ConvertNgay(_songay[1]).ToString());
            col2de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[1]));
        }

        private void col3de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32( GetCurrentColumnValue("MaKPnx"));
            col3de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[2]));
        }

        private void col4de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32( GetCurrentColumnValue("MaKPnx"));
            col4de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[3]));
        }

        private void col5de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32( GetCurrentColumnValue("MaKPnx"));
            col5de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[4]));
        }


        private void col6de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col6de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[5]));
        }

        private void col7de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col7de.Text = SL(_madv, _makp,DungChung.Ham.ConvertNgay(_songay[6]));
        }

        private void col8de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col8de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[7]));
        }

        private void col9de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col9de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[8]));
        }

        private void col10de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col10de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[9]));
        }

        private void col11de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col11de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[10]));
        }
        private void col12de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col12de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[11]));
        }
        private void col13de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col13de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[12]));
        }
        private void col14de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col14de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[13]));


        }
        private void col15de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col15de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[14]));
        }
        private void col16de_BeforePrint(object sender, CancelEventArgs e)
        {
            int _madv = 0;
            int _makp = 0;
            if (GetCurrentColumnValue("MaDV") != null)
                _madv = Convert.ToInt32(GetCurrentColumnValue("MaDV"));
            if (GetCurrentColumnValue("MaKPnx") != null)
                _makp = Convert.ToInt32(GetCurrentColumnValue("MaKPnx"));
            col16de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[15]));
        }


        private void col1Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            col1Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[0]));
        }   

        private void col2Tong_BeforePrint(object sender, CancelEventArgs e)
        {
           
            col2Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[1]));
        }

        private void col3Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            ////int _madv = 0;
            ////if (GetCurrentColumnValue("MaDV") != null)
            col3Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[2]));
        }

        private void col4Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col4Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[3]));
        }

        private void col5Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col5Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[4]));
        }

        private void col6Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col6Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[5]));
        }

        private void col7Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col7Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[6]));
        }
        private void col8Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col8Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[7]));
        }
        private void col9Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col9Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[8]));
        }
        private void col10Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col10Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[9]));
        }
        private void col11Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col11Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[10]));
        }
        private void col12Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col12Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[11]));
        }
        private void col13Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            ////int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col13Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[12]));
        }
        private void col14Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col14Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[13]));
        }
        private void col15Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col15Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[14]));
        }
        private void col16Tong_BeforePrint(object sender, CancelEventArgs e)
        {
            //string _makp = "";
            //if (GetCurrentColumnValue("MaKPnx") != null)
            //    _makp = GetCurrentColumnValue("MaKPnx").ToString();
            //int _madv = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv = GetCurrentColumnValue("MaDV").ToString();
            col16Tong.Text = SLNgay(makhoa, DungChung.Ham.ConvertNgay(_songay[15]));
        }



       
    }
}
