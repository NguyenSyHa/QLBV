using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Phieulinhchokhoa_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        public Phieulinhchokhoa_A5()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenHH.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:00}";
            }
            else
            {
                colsoluongyc.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            if (DungChung.Bien.MaBV == "24009" )
            {
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:00}";
            }
            colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
            //colTsongluongG.DataBindings.Add("Text", DataSource, "SoLuong").FormatString=DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            Benhvien.Value = DungChung.Bien.TenCQ;
            Boyte.Value = DungChung.Bien.TenCQCQ;


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
            //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;.
          
            if(DungChung.Bien.MaBV == "26007")
            {
                xrLabel6.Visible = true;
            }
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenkp="";
            if(Khoa.Value!=null)
                tenkp=Khoa.Value.ToString();
            var kp = _data.KPhongs.Where(p => p.TenKP == tenkp).Select(p => p.PLoai).ToList();
            if (kp.Count>0 && kp.First()=="Cận lâm sàng")
            {
                xrTableCell29.Text = "TRƯỞNG KHOA C.LÂM SÀNG";
            }
            else
            {
                xrTableCell29.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }
        }
    }

}
