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
    public partial class rep_Tk30NgaySDThuoc_TT23_HTTT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_Tk30NgaySDThuoc_TT23_HTTT()
        {
            InitializeComponent();
        }
           string[] _songay;

           List<QLBV.FormThamSo.frmTk15NgaySDThuoc_TT23.l_CTThuoc> _ldtct = new List<QLBV.FormThamSo.frmTk15NgaySDThuoc_TT23.l_CTThuoc>();
        

          public rep_Tk30NgaySDThuoc_TT23_HTTT(string[] ngay)
        {
            InitializeComponent();
            _songay = ngay;
          
          
           
        }
        
        public void BindingData()
        {
            
            colTenNhomDVGh1.DataBindings.Add("Text", DataSource, "TenNhomDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            txtMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:#,###}";
            COLqcpc.DataBindings.Add("Text", DataSource, "QCPC");
            celThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            cel_TongTienCong.DataBindings.Add("Text", DataSource, "ThanhTien");//.FormatString 
            cel_TongTienCong.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel_TongTienCong_G.DataBindings.Add("Text", DataSource, "ThanhTien");//.FormatString 
            cel_TongTienCong_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhomDV"));
             int  i=0;

                 
                 foreach (XRTableCell cell in xrTableRow2)
                 {
                     if (i >= 31)
                         break;
                     if (cell.Index == i + 4)
                     {                        
                         string fieldName = "SL" + i.ToString();
                         cell.DataBindings.Add("Text", DataSource, fieldName).FormatString = "{0:#,###}";
                         i++;
                     }
                 }
             
        }
      
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
          
            colSoTT.Text = stt.ToString();
           
            
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
            //int i = 0;
            
            //double tongSL = 0;
            //foreach (XRTableCell cell in xrTableRow2)
            //{
               
            //    if (cell.Index == i + 4)
            //    {
            //        double sl = 0;
            //        sl = cell.Text == "" ? 0 : Convert.ToDouble(cell.Text);
            //        tongSL = tongSL + sl;
            //        i++;
            //    }

            //}
            //colSoLuong.Text = tongSL.ToString();

        }

    }
}
