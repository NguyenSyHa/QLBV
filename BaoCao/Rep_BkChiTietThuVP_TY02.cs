using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BkChiTietThuVP_TY02 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BkChiTietThuVP_TY02()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenKPG.DataBindings.Add("Text", DataSource, "TenKP");


            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colDChi.DataBindings.Add("Text", DataSource, "DChi");
            colTienGiuong.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = DungChung.Bien.FormatString[1];
            colThuoc.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuThuat.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colMauCP.DataBindings.Add("Text", DataSource, "MauCP").FormatString = DungChung.Bien.FormatString[1];
            colKSK.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            colCDHA.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colVTYT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colXN.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            colKhac.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBH.DataBindings.Add("Text", DataSource, "NgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colVPBHYT.DataBindings.Add("Text", DataSource, "TienBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTong.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];

            colTienGiuongG.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = DungChung.Bien.FormatString[1];
            colThuocG.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuThuatG.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colMauCPG.DataBindings.Add("Text", DataSource, "MauCP").FormatString = DungChung.Bien.FormatString[1];
            colKSKG.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            colCDHAG.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colVTYTG.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colXNG.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            colKhacG.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHG.DataBindings.Add("Text", DataSource, "NgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colVPBHYTG.DataBindings.Add("Text", DataSource, "TienBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTongG.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];


            colTienGiuongT.DataBindings.Add("Text", DataSource, "TienGiuong").FormatString = DungChung.Bien.FormatString[1];
            colThuocT.DataBindings.Add("Text", DataSource, "Thuoc").FormatString = DungChung.Bien.FormatString[1];
            colThuThuatT.DataBindings.Add("Text", DataSource, "TTPT").FormatString = DungChung.Bien.FormatString[1];
            colMauCPT.DataBindings.Add("Text", DataSource, "MauCP").FormatString = DungChung.Bien.FormatString[1];
            colKSKT.DataBindings.Add("Text", DataSource, "KSK").FormatString = DungChung.Bien.FormatString[1];
            colCDHAT.DataBindings.Add("Text", DataSource, "CDHA").FormatString = DungChung.Bien.FormatString[1];
            colVTYTT.DataBindings.Add("Text", DataSource, "VTYT").FormatString = DungChung.Bien.FormatString[1];
            colXNT.DataBindings.Add("Text", DataSource, "XN").FormatString = DungChung.Bien.FormatString[1];
            colKhacT.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            colCPNgoaiBHT.DataBindings.Add("Text", DataSource, "NgoaiBH").FormatString = DungChung.Bien.FormatString[1];
            colVPBHYTT.DataBindings.Add("Text", DataSource, "TienBHYT").FormatString = DungChung.Bien.FormatString[1];
            colTongT.DataBindings.Add("Text", DataSource, "Tong").FormatString = DungChung.Bien.FormatString[1];


            GroupHeader1.GroupFields.Add(new GroupField("TenKP"));


        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void colDChi_BeforePrint(object sender, CancelEventArgs e)
        {
            //    if (this.GetCurrentColumnValue("TienGiuong") != null)
            //    {
            //        if (this.GetCurrentColumnValue("TienGiuong").ToString() == "0")
            //        {
            //            colTienGiuong.Text = "";
            //        }
            //        else
            //        {
            //            colTienGiuong.Text = this.GetCurrentColumnValue("TienGiuong").ToString();
            //        }
            //    }
            //    else
            //    { colTienGiuong.Text = ""; }
            //    int thuoc = 0;
            //    if (this.GetCurrentColumnValue("Thuoc") != null)
            //    {
            //        if (this.GetCurrentColumnValue("Thuoc").ToString() == "0")
            //        {

            //            colThuoc.Text = "1";
            //        }
            //        //else
            //        //{
            //        //    colThuoc.Text = this.GetCurrentColumnValue("Thuoc").ToString();
            //        //}
            //    }

            //    else
            //    {
            //        thuoc = Convert.ToInt32(this.GetCurrentColumnValue("Thuoc"));
            //        colThuoc.Text=this.GetCurrentColumnValue("Thuoc").ToString(); 
            //    }

            //}
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNLBieu.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
    }
}
