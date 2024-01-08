using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace QLBV.BaoCao
{
    public partial class Rep_DSBenhNhanKSK : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DSBenhNhanKSK()
        {
            InitializeComponent();
            //_lds = ds;
        }
      
        private void repsothekho_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
     
   
        private void xrTableCell19_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void colSCTn_BeforePrint(object sender, CancelEventArgs e)
        {
            //colDiengiai.Text = "";
        }
        public void BindingData()
        {
            colTenBNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            colNamSinh.DataBindings.Add("Text", DataSource, "NamSinh");
            colGtinh.DataBindings.Add("Text", DataSource, "GTinh");
            colCLS.DataBindings.Add("Text", DataSource, "TenDV");
            colKhoaPhong.DataBindings.Add("Text", DataSource, "tenkp");
            colHinhThucKSK.DataBindings.Add("Text", DataSource, "TChung");
            colKetLuan.DataBindings.Add("Text", DataSource, "KetLuan");
            colNgayKham.DataBindings.Add("Text", DataSource, "NNhap");
            colTenCB.DataBindings.Add("Text", DataSource, "ChuyenKhoa");
            colDChi.DataBindings.Add("Text", DataSource, "DChi");
        }
        //int tdk = 0;
        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }
       // int td=0;
        private void colSoluongton_BeforePrint(object sender, CancelEventArgs e)
        {

            //colKP.Text = sltck.ToString();
        }

        private void colPhanloai_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void colNgaythang_BeforePrint(object sender, CancelEventArgs e)
        {

            
        }

        private void colNgayt_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void xrLabel5_BeforePrint(object sender, CancelEventArgs e)
        {
            //sltdk();
        }
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }


    }
}
