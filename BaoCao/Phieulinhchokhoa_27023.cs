using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Phieulinhchokhoa_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public Phieulinhchokhoa_27023()
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
            if (DungChung.Bien.MaBV == "24009")
            {
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:00}";
            }
            if (DungChung.Bien.MaBV == "27023")
            {
                colsoluongtp.DataBindings.Add("Text", DataSource, "SoLuong");
            }
            colMaDV.DataBindings.Add("Text", DataSource, "MaTam");
            //colTsongluongG.DataBindings.Add("Text", DataSource, "SoLuong").FormatString=DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            celSoLo.DataBindings.Add("Text", DataSource, "SoLo");
            celHanDung.DataBindings.Add("Text", DataSource, "HanDung").FormatString = "{0: dd/MM/yyyy}";

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            Benhvien.Value = DungChung.Bien.TenCQ;
            Boyte.Value = DungChung.Bien.TenCQCQ;


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12121")
                xrTable6.Visible = false;
            if (Ngaythang.Value != null && Ngaythang.Value.ToString() != "")
                txtNgayThang.Text = "Ngày ... tháng ... năm 20...";
            //txtNgayThang.Text = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;.
            if (Ycu.Value.ToString() == "7")
            {
                xrTable6.Visible = true;
            }
            else
            {
                xrTable6.Visible = false;
            }
            if (DungChung.Bien.MaBV == "26007")
            {
                xrLabel6.Visible = true;
            }
            QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            string tenkp = "";
            if (Khoa.Value != null)
                tenkp = Khoa.Value.ToString();
            var kp = _data.KPhongs.Where(p => p.TenKP == tenkp).Select(p => p.PLoai).ToList();
            if (kp.Count > 0 && kp.First() == "Cận lâm sàng")
            {
                xrTableCell14.Text = "TRƯỞNG KHOA C.LÂM SÀNG";
            }
            else
            {
                xrTableCell14.Text = "TRƯỞNG KHOA LÂM SÀNG";
            }
        }
    }

}
