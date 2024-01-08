using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class PhieulinhthuocVTYT_NgoaiTru : DevExpress.XtraReports.UI.XtraReport
    {
        int _dongy = 0;
        public PhieulinhthuocVTYT_NgoaiTru()
        {
            InitializeComponent();
        }
        public PhieulinhthuocVTYT_NgoaiTru(int dy)
        {
            InitializeComponent();
            _dongy = dy;
        }
        public void BindingData()
        {
            string _fomat = DungChung.Bien.FormatString[0];
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
                _fomat = "{0:00}";
            else
                _fomat = "{0:##,###.##}";
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = _fomat;
            }
            else {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
            }          
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");           
            colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
          
            

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
           txtTenCQCQ.Text= DungChung.Bien.TenCQCQ.ToUpper();    
        }

       
    }
     
}
