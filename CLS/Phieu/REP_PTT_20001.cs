using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class REP_PTT_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public REP_PTT_20001()
        {
            InitializeComponent();
        }
       //public void bindingdata()
       // {
       //     //lab_sovv.DataBindings.Add("Text", DataSource, "SoVV");
       //     //col_ten.DataBindings.Add("Text", DataSource, "TenBNhan");
       //     //col_tuoi.DataBindings.Add("Text", DataSource, "Tuoi");
       //     //col_goitinh.DataBindings.Add("Text", DataSource, "GTinh");
       //     //col_khoa.DataBindings.Add("Text", DataSource, "TenKP");
       //     //col_buong.DataBindings.Add("Text", DataSource, "Buong");
       //     //col_giuong.DataBindings.Add("Text", DataSource, "Giuong");
       //     //col_dtuong.DataBindings.Add("Text", DataSource, "DTuong");
       //     //col_chandoan.DataBindings.Add("Text", DataSource, "ChanDoan");
       //     //col_maicd.DataBindings.Add("Text", DataSource, "MaICD");
       //     //col_tencd.DataBindings.Add("Text", DataSource, "TenDV");
       //     //col_loaitt.DataBindings.Add("Text", DataSource, "Loai");
       //     //col_cbchinh.DataBindings.Add("Text", DataSource, "TenCB");
       //     //col_ttvien.DataBindings.Add("Text", DataSource, "TenCB");
       //     //col_solan.DataBindings.Add("Text", DataSource, "SoLuong");
          
       // }

       private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
       {
 

       }

       private void col_chandoan_BeforePrint(object sender, CancelEventArgs e)
       {

       }
    }
}
