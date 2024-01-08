using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BkeTTVPRaVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BkeTTVPRaVien()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celMaBNhan.DataBindings.Add("Text", DataSource, "MaBNhan");    
            celHoTen.DataBindings.Add("Text", DataSource, "TenBNhan");      
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celTamUng1.DataBindings.Add("Text", DataSource, "QuyenHD").FormatString = DungChung.Bien.FormatString[1];
            celTamUng2.DataBindings.Add("Text", DataSource, "SoHDTU").FormatString = DungChung.Bien.FormatString[1];
            celTamUng3.DataBindings.Add("Text", DataSource, "SoTienTU").FormatString = DungChung.Bien.FormatString[1];
            celVienPhi1.DataBindings.Add("Text", DataSource, "SoHDVP").FormatString = DungChung.Bien.FormatString[1];
            celVienPhi2.DataBindings.Add("Text", DataSource, "SoTienVP").FormatString = DungChung.Bien.FormatString[1];
            celSoTien1.DataBindings.Add("Text", DataSource, "Thu").FormatString = DungChung.Bien.FormatString[1];
            celSoTien2.DataBindings.Add("Text", DataSource, "chi").FormatString = DungChung.Bien.FormatString[1];

            celTamUngR3.DataBindings.Add("Text", DataSource, "SoTienTU").FormatString = DungChung.Bien.FormatString[1];
            celVienPhiR2.DataBindings.Add("Text", DataSource, "SoTienVP").FormatString = DungChung.Bien.FormatString[1];
            celSoTienR1.DataBindings.Add("Text", DataSource, "Thu").FormatString = DungChung.Bien.FormatString[1];
            celSoTienR2.DataBindings.Add("Text", DataSource, "chi").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcqcq.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtcq.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
