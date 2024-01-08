using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Phieulinhchokhoa_A5_2lien : DevExpress.XtraReports.UI.XtraReport
    {
        public Phieulinhchokhoa_A5_2lien()
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

          
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");


            colTenHH2.DataBindings.Add("Text", DataSource, "TenDV");
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "30003")
            {
                colsoluongyc2.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:00}";
            }
            else
            {
                colsoluongyc2.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                colsoluongtp2.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:00}";
            }
            colMaDV2.DataBindings.Add("Text", DataSource, "MaTam");
            colDVT2.DataBindings.Add("Text", DataSource, "DonVi");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            Benhvien.Value = DungChung.Bien.TenCQ;
            Boyte.Value = DungChung.Bien.TenCQCQ;


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
            {
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
                txtNgayThang2.Text = "Ngày ... tháng ... năm 20...";
            }
            //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;.
            if (Ycu.Value.ToString() == "7")
            {
                xrTable6.Visible = true;
            }
            else
            {
                xrTable6.Visible = false;
            }
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
                xrTableCell14.Text = "TRƯỞNG KHOA C.LÂM SÀNG";
            }
            else
            {
                xrTableCell14.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }
        }

        private void ReportHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
            {
                txtNgayThang3.Text = "Ngày ... tháng ... năm 20...";
                txtNgayThang4.Text = "Ngày ... tháng ... năm 20...";
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                PageFooter.Visible = true;
                SubBand1.Visible = false;
            }
        }
    }

}
