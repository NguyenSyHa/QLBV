using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
namespace QLBV.BaoCao
{
    public partial class rep_Tk15NgaySDThuoc_TT23 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_Tk15NgaySDThuoc_TT23()
        {
            InitializeComponent();
        }
           string[] _songay;

           List<QLBV.FormThamSo.frmTk15NgaySDThuoc_TT23.l_CTThuoc> _ldtct = new List<QLBV.FormThamSo.frmTk15NgaySDThuoc_TT23.l_CTThuoc>();
          bool _InTrang2 = false;

          //public rep_PhieuCongKhaiThuoc_TT23(string[] ngay, List<QLBV.FormThamSo.frmTk15NgaySDThuoc_NB01.l_CTThuoc> CTThuoc, bool _in)
          public rep_Tk15NgaySDThuoc_TT23(string[] ngay,  bool _in)
        {
            InitializeComponent();
            _songay = ngay;
          
           // _ldtct = CTThuoc;
            _InTrang2 = _in;
        }
        
        public void BindingData()
        {
            
            colTenNhomDVGh1.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            COLqcpc.DataBindings.Add("Text", DataSource, "QCPC");
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));
             int  i=0;

             if (_InTrang2)
             {
                 foreach (XRTableCell cell in xrTableRow2)
                 {
                     if (i >= 16)
                         break;
                     if (cell.Index == i + 4)
                     {
                         //if(_InTrang2)
                         //    cell.Text = DataBindings.Add("Text", DataSource, "SoLuong"+ (i+16).ToString()).FormatString = DungChung.Bien.FormatString[0]; 
                         //else
                         string fieldName = "SL" + (i+16).ToString();
                         cell.DataBindings.Add("Text", DataSource, fieldName).FormatString ="{0:#,###}";
                         i++;
                     }
                 }
             }
             else
             {
                 foreach (XRTableCell cell in xrTableRow2)
                 {
                     if (i >= 16)
                         break;
                     if (cell.Index == i + 4)
                     {
                         //if(_InTrang2)
                         //    cell.Text = DataBindings.Add("Text", DataSource, "SoLuong"+ (i+16).ToString()).FormatString = DungChung.Bien.FormatString[0]; 
                         //else
                         string fieldName = "SL" + i.ToString();
                         cell.DataBindings.Add("Text", DataSource, fieldName).FormatString = "{0:#,###}";
                         i++;
                     }
                 }
             }
        }
        private string SL(int MaDV, double DonGia, DateTime ngay)
        {
            
            string soluong = "";
            var dt2 = _ldtct.Where(p => p.NgayKe.Day == ngay.Day).Where(p => p.NgayKe.Month == ngay.Month).Where(p => p.NgayKe.Year == ngay.Year).Where(p => p.DonGia == DonGia).Where(p => p.MaDV == MaDV).Sum(p => p.SoLuong);
            if (dt2 != null && dt2.ToString() != "" && dt2.ToString()!="0")
            {
                soluong = dt2.ToString();
                return soluong;
            }
            else
                return null;
            
        }
        #region bỏ
        //private static string SL2(int madv, double DonGia, DateTime ngay,string mbn)
        //{
            
        //    QLBV_Database.QLBVEntities _Data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        //    string soluong = "";
        //    var dt2 = (from dthc in _Data.DThuocs.Where(p=>p.MaBNhan== (mbn))
        //              join dtct in _Data.DThuoccts.Where(p=>p.TrongBH==1 || p.TrongBH==0) on dthc.IDDon equals dtct.IDDon
        //              where (dtct.MaDV== (MaDV) && dtct.DonGia == DonGia)
        //              where (dthc.NgayKe.Value.Day == ngay.Day && dthc.NgayKe.Value.Month == ngay.Month && dthc.NgayKe.Value.Year == ngay.Year)
        //              group dtct by dtct.DonGia into kq
        //              select new {SoLuong=kq.Sum(p=>p.SoLuong) }).ToList();
        //    if (dt2.Count > 0) {
        //        if (dt2.First().SoLuong > 0)
        //        {
        //            soluong = dt2.First().SoLuong.ToString();
        //        }
        //    }
        //     return soluong;                                    
        //}
        #endregion
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
            colCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
             colCQ.Text = DungChung.Bien.TenCQ.ToUpper() ;
            string[] sngay = new string[50];
            for (int a = 0; a < 50; a++)
            {
                sngay[a] = "";
            }


            for (int i = 0; i < _songay.Length; i++)
            {
                  if(_songay[i] != null)
                sngay[i] = _songay[i];
            }
            
            int  k=0;

            foreach (XRTableCell cell in xrTableRow3)
            {
                cell.Text = "";
                if (k >=16)
                    break;
              if(  cell.Index==k)
              {
                  if (sngay[k].Length >= 5)
                  cell.Text = sngay[k].ToString().Substring(0, 5);
                    k++;  
              }
               
            }
           
        }
        int stt = 0;
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            stt++;
            //double _soLuong = 0;
            //int _madv = 0;
            //double DonGia = 0;
            //if (GetCurrentColumnValue("MaDV") != null)
            //    _madv =Convert.ToInt32( GetCurrentColumnValue("MaDV"));
            //if (GetCurrentColumnValue("DonGia") != null)
            //    DonGia = Convert.ToDouble(GetCurrentColumnValue("DonGia"));
            //foreach (var a in _songay)
            //{
            //    if (SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a)) != null && SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a)) != "")
            //        _soLuong += Convert.ToDouble(SL(_madv, DonGia, DungChung.Ham.ConvertNgay(a)));
            //}
            //if (_soLuong == 0)
            //{
            //    xrTable2.Visible = false;
            //    xrLine1.Visible = false;
            //}
            //else
            //{
            //    stt++;
            //    xrTable2.Visible = true;
            //    xrLine1.Visible = true;
            //}
            colSoTT.Text = stt.ToString();
            ////
            //int  i=0;
       
            //foreach (XRTableCell cell in xrTableRow2) {
            //    if (i >= 16)
            //        break;
            //  if(  cell.Index==i+3)
            //  {
            //        cell.Text = SL(_madv, DonGia, DungChung.Ham.ConvertNgay(_songay[i]));
            //        i++;  
            //    }
               
            //}
            ////
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //colMaDV.Text = stt.ToString();
            celNguoiThongKe.Text = DungChung.Bien.NguoiLapBieu;
            celTruongKhoaLS.Text = DungChung.Bien.TruongKhoaLS;
            colngaythang.Text = "Ngày ....... tháng ...... năm ........";
        }

        private void colSoLuong_BeforePrint(object sender, CancelEventArgs e)
        {
            int i = 0;
            
            double tongSL = 0;
            foreach (XRTableCell cell in xrTableRow2)
            {
                if (i >= 16)
                    break;
                if (cell.Index == i + 4)
                {
                    double sl = 0;
                    sl = cell.Text == "" ? 0 : Convert.ToDouble(cell.Text);
                    tongSL = tongSL + sl;
                    i++;
                }

            }
            colSoLuong.Text = tongSL.ToString();

        }

    }
}
