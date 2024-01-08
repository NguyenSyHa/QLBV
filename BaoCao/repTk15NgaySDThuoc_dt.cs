using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repTk15NgaySDThuoc_dt : DevExpress.XtraReports.UI.XtraReport
    {
        public repTk15NgaySDThuoc_dt()
        {
            InitializeComponent();  
        }
     
    
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            txtKhoa.DataBindings.Add("Text", DataSource, "MaKPnx");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colQuyCach.DataBindings.Add("Text", DataSource, "QuyCach");
            colTong.DataBindings.Add("Text", DataSource, "Slt");
            colTongTong.DataBindings.Add("Text", DataSource, "Slt");
            col1de.DataBindings.Add("Text",DataSource,"Sl1");
            col2de.DataBindings.Add("Text", DataSource, "Sl2");
            col3de.DataBindings.Add("Text", DataSource, "Sl3");
            col4de.DataBindings.Add("Text", DataSource, "Sl4");
            col5de.DataBindings.Add("Text", DataSource, "Sl5");
            col6de.DataBindings.Add("Text", DataSource, "Sl6");
            col7de.DataBindings.Add("Text", DataSource, "Sl7");
            col8de.DataBindings.Add("Text", DataSource, "Sl8");
            col9de.DataBindings.Add("Text", DataSource, "Sl9");
            col10de.DataBindings.Add("Text", DataSource, "Sl10");
            col11de.DataBindings.Add("Text", DataSource, "Sl11");
            col12de.DataBindings.Add("Text", DataSource, "Sl12");
            col13de.DataBindings.Add("Text", DataSource, "Sl13");
            col14de.DataBindings.Add("Text", DataSource, "Sl14");
            col15de.DataBindings.Add("Text", DataSource, "Sl15");
            col16de.DataBindings.Add("Text", DataSource, "Sl16");
            col1Tong.DataBindings.Add("Text", DataSource, "Sl1");
            col2Tong.DataBindings.Add("Text", DataSource, "Sl2");
            col3Tong.DataBindings.Add("Text", DataSource, "Sl3");
            col4Tong.DataBindings.Add("Text", DataSource, "Sl4");
            col5Tong.DataBindings.Add("Text", DataSource, "Sl5");
            col6Tong.DataBindings.Add("Text", DataSource, "Sl6");
            col7Tong.DataBindings.Add("Text", DataSource, "Sl7");
            col8Tong.DataBindings.Add("Text", DataSource, "Sl8");
            col9Tong.DataBindings.Add("Text", DataSource, "Sl9");
            col10Tong.DataBindings.Add("Text", DataSource, "Sl10");
            col11Tong.DataBindings.Add("Text", DataSource, "Sl11");
            col12Tong.DataBindings.Add("Text", DataSource, "Sl12");
            col13Tong.DataBindings.Add("Text", DataSource, "Sl13");
            col14Tong.DataBindings.Add("Text", DataSource, "Sl14");
            col15Tong.DataBindings.Add("Text", DataSource, "Sl15");
            col16Tong.DataBindings.Add("Text", DataSource, "Sl16");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colCQ.Text = DungChung.Bien.TenCQ;
          
            //  QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            //string[] sngay = new string[60];
            //for (int a = 0; a <= 50; a++)
            //{
            //    sngay[a] = "01/01/2000";
            //}
           
           
            //for (int i = 0; i < _songay.Length;i++ )
            //{
            //    sngay[i] = _songay[i];
            //}
            //if(sngay[0].Length>=8)
            //col1.Text =DungChung.Ham.ConvertNgay(sngay[0]).Day.ToString();
            //if (sngay[1].Length >= 8)
            //    col2.Text = DungChung.Ham.ConvertNgay(sngay[1]).Day.ToString();
            //if (sngay[2].Length >= 8)
            //    col3.Text = DungChung.Ham.ConvertNgay(sngay[2]).Day.ToString();
            //if (sngay[3].Length >= 8)
            //    col4.Text = DungChung.Ham.ConvertNgay(sngay[3]).Day.ToString();
            //if (sngay[4].Length >= 8)
            //    col5.Text = DungChung.Ham.ConvertNgay(sngay[4]).Day.ToString();
            //if (sngay[5].Length >= 8)
            //    col6.Text = DungChung.Ham.ConvertNgay(sngay[5]).Day.ToString();
            //if (sngay[6].Length >= 8)
            //    col7.Text = DungChung.Ham.ConvertNgay(sngay[6]).Day.ToString();
            //if (sngay[7].Length >= 8)
            //    col8.Text = DungChung.Ham.ConvertNgay(sngay[7]).Day.ToString();
            //if (sngay[8].Length >= 8)
            //    col9.Text = DungChung.Ham.ConvertNgay(sngay[8]).Day.ToString();
            //if (sngay[9].Length >= 8)
            //    col10.Text = DungChung.Ham.ConvertNgay(sngay[9]).Day.ToString();
            //if (sngay[10].Length >= 8)
            //    col11.Text = DungChung.Ham.ConvertNgay(sngay[10]).Day.ToString();
            //if (sngay[11].Length >= 8)
            //    col12.Text = DungChung.Ham.ConvertNgay(sngay[11]).Day.ToString();
            //if (sngay[12].Length >= 8)
            //    col13.Text = DungChung.Ham.ConvertNgay(sngay[12]).Day.ToString();
            //if (sngay[13].Length >= 8)
            //    col14.Text = DungChung.Ham.ConvertNgay(sngay[13]).Day.ToString();
            //if (sngay[14].Length >= 8)
            //    col15.Text = DungChung.Ham.ConvertNgay(sngay[14]).Day.ToString();
            //if (sngay[15].Length >= 8)
            //    col16.Text = DungChung.Ham.ConvertNgay(sngay[15]).Day.ToString();
            //colCQCQ.Text = DungChung.Bien.TenCQCQ;
            //colCQ.Text = DungChung.Bien.TenCQ;


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiThongKe.Text = DungChung.Bien.NguoiLapBieu;
            colKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
            colTKLamSang.Text = DungChung.Bien.TruongKhoaLS;
            if (DungChung.Bien.MaBV == "08204")
                colKDUoc.Text = "Kế toán dược".ToUpper();
        }
      
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
        //private string SL(int madv,string makp, DateTime ngay)
        //{
        //    QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon); 
        //    string soluong = "";
            
        //    if (string.IsNullOrEmpty(makp))
        //    {
        //        var sl = (from dt in Data.DThuocs.Where(p=>p.NgayKe.Value.Day == ngay.Day && p.NgayKe.Value.Month == ngay.Month && p.NgayKe.Value.Year == ngay.Year).Where(p=>p.MaKXuat==makhoa).Where(p=>p.Status==_st||p.Status==_st1)
        //                  join dtct in Data.DThuoccts.Where(p => p.MaDV== (MaDV)) on dt.IDDon equals dtct.IDDon
        //                  group new { dt, dtct } by new {dt.Status, dtct.MaDV, dtct.DonVi } into kq
        //                  select new {kq.Key.Status, kq.Key.DonVi, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).ToList();
        //        if (sl.Count > 0)
                  
        //                soluong = sl.First().SoLuong.ToString();
                     
        //        return soluong;
        //    }
        //    else {
        //        var sl = (from dt in Data.DThuocs.Where(p => p.NgayKe.Value.Day == ngay.Day && p.NgayKe.Value.Month == ngay.Month && p.NgayKe.Value.Year == ngay.Year).Where(p => p.MaKP == makp).Where(p => p.MaKXuat == makhoa).Where(p => p.Status == _st || p.Status == _st1)
        //                  join dtct in Data.DThuoccts.Where(p => p.MaDV== (MaDV)) on dt.IDDon equals dtct.IDDon
        //                  group new { dt, dtct } by new {dt.Status, dtct.MaDV, dtct.DonVi } into kq
        //                  select new {kq.Key.Status, kq.Key.DonVi, kq.Key.MaDV, SoLuong = kq.Sum(p => p.dtct.SoLuong) }).ToList();
        //        if (sl.Count > 0)
                   
        //                soluong = sl.First().SoLuong.ToString();

        //        return soluong;
        //    }
        //}
        //private string SLNgay(string makp, DateTime ngay)
        //{
        //    QLBV_Database.QLBVEntities Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    string soluongngay = "";
        //    if (string.IsNullOrEmpty(makp))
        //    {
        //        var sl = (from dt in Data.DThuocs.Where(p => p.NgayKe.Value.Day == ngay.Day && p.NgayKe.Value.Month == ngay.Month && p.NgayKe.Value.Year == ngay.Year).Where(p => p.MaKXuat == makhoa).Where(p => p.Status == _st || p.Status == _st1)
        //                  join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
        //                  group new { dt, dtct } by new { dt.Status, dt.NgayKe.Value.Day, dt.NgayKe.Value.Month } into kq
        //                  select new { kq.Key.Status, SoLuongX = kq.Sum(p => p.dtct.SoLuong) }).ToList();
        //        if (sl.Count > 0)

        //            soluongngay = sl.Sum(p => p.SoLuongX).ToString();

        //    }
        //    else
        //    {
        //        var sl = (from dt in Data.DThuocs.Where(p => p.NgayKe.Value.Day == ngay.Day && p.NgayKe.Value.Month == ngay.Month && p.NgayKe.Value.Year == ngay.Year).Where(p => p.MaKP == makp).Where(p => p.MaKXuat == makhoa).Where(p => p.Status == _st || p.Status == _st1)
        //                  join dtct in Data.DThuoccts on dt.IDDon equals dtct.IDDon
        //                  group new { dt, dtct } by new { dt.Status, dt.NgayKe.Value.Day, dt.NgayKe.Value.Month } into kq
        //                  select new { kq.Key.Status, SoLuongX = kq.Sum(p => p.dtct.SoLuong) }).ToList();
        //        if (sl.Count > 0)

        //            soluongngay = sl.Sum(p => p.SoLuongX).ToString();

        //    }
        //       // soluongngay = sl.Sum(p => p.SoLuongX).ToString();
        //    return soluongngay;
        }

        //private void col1de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    string _madv="";
        //     if(GetCurrentColumnValue("MaDV")!=null)
        //     _madv = GetCurrentColumnValue("MaDV").ToString();
        //         int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col1de.Text= SL(_madv,_makp,DungChung.Ham.ConvertNgay(_songay[0]));
        //}

        //private void col2de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col2de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[1]));
        //}

        //private void col3de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col3de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[2]));
        //}

        //private void col4de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //     if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //   _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //   col4de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[3]));
        //}

        //private void col5de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col5de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[4]));
        //}


        //private void col6de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //   col6de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[5]));
        //}

        //private void col7de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //     if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col7de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[6]));
        //}

        //private void col8de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //     col8de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[7]));
        //}

        //private void col9de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col9de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[8]));
        //}

        //private void col10de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)          
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col10de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[9]));
        //}

        //private void col11de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col11de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[10]));
        //}
        //private void col12de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //     if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col12de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[11]));
        //}
        //private void col13de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col13de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[12]));
        //}
        //private void col14de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //     col14de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[13]));


        //}
        //private void col15de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col15de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[14]));
        //}
        //private void col16de_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _madv = 0;
        //    //int sta = -10;
        //    if (GetCurrentColumnValue("MaDV") != null)
        //        _madv = GetCurrentColumnValue("MaDV").ToString();
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //   // if (_st != null) { sta = Convert.ToInt32(_st); }
        //    col16de.Text = SL(_madv, _makp, DungChung.Ham.ConvertNgay(_songay[15]));
        //}


        //private void col1Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col1Tong.Text = SLNgay( _makp, DungChung.Ham.ConvertNgay(_songay[0]));
        //}   

        //private void col2Tong_BeforePrint(object sender, CancelEventArgs e)
        //{

        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString(); 
        //    col2Tong.Text = SLNgay( makhoa, DungChung.Ham.ConvertNgay(_songay[1]));
        //}

        //private void col3Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //   col3Tong.Text = SLNgay( _makp, DungChung.Ham.ConvertNgay(_songay[2]));
        //}

        //private void col4Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
          
        //    col4Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[3]));
        //}

        //private void col5Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
        //    col5Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[4]));
        //}

        //private void col6Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
         
        //    col6Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[5]));
        //}

        //private void col7Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
           
        //    col7Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[6]));
        //}
        //private void col8Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
          
        //    col8Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[7]));
        //}
        //private void col9Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
           
        //    col9Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[8]));
        //}
        //private void col10Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
           
        //    col10Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[9]));
        //}
        //private void col11Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
         
        //    col11Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[10]));
        //}
        //private void col12Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
         
        //    col12Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[11]));
        //}
        //private void col13Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
           
        //    col13Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[12]));
        //}
        //private void col14Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
          
        //    col14Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[13]));
        //}
        //private void col15Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
            
        //    col15Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[14]));
        //}
        //private void col16Tong_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    int _makp = 0;
        //    if (GetCurrentColumnValue("MaKPnx") != null)
        //        _makp = GetCurrentColumnValue("MaKPnx").ToString();
           
        //    col16Tong.Text = SLNgay(_makp, DungChung.Ham.ConvertNgay(_songay[15]));
        //}



       
    
}
