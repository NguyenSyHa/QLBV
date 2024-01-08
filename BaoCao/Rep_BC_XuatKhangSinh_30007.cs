using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_XuatKhangSinh_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_XuatKhangSinh_30007()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celKhoaDuoc.Text = "Họ tên: " + DungChung.Bien.TruongKhoaDuoc;
            celKeToan.Text = "Họ tên: " + DungChung.Bien.KeToanTruong;
            celGiamDoc.Text = "Họ tên: " + DungChung.Bien.GiamDoc;
            celNgayThang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }

        public void BindingData()
        {
            //group
            celSTT.DataBindings.Add("Text", DataSource, "SoTTqd");
            celTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            celMaATC.DataBindings.Add("Text", DataSource, "MaATC");
            celTTBietDuoc.DataBindings.Add("Text", DataSource, "SoTT");
            celTenBietDuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            celNongDo.DataBindings.Add("Text", DataSource, "HamLuong");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celDuongDung.DataBindings.Add("Text", DataSource, "DuongD");
            //celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            //celDonGia.DataBindings.Add("Text", DataSource, "DonGia");

            celSoTT_CT.DataBindings.Add("Text", DataSource, "SoTTqd");
          //  celTenHC_CT.DataBindings.Add("Text", DataSource, "TenHC");
            lblTenHC.DataBindings.Add("Text", DataSource, "TenHC");
            //celMaATC_CT.DataBindings.Add("Text", DataSource, "MaATC");
            lblMaATC.DataBindings.Add("Text", DataSource, "MaATC");
            //celTTBietDuoc_CT.DataBindings.Add("Text", DataSource, "SoTT");
            lblTTBietDuoc.DataBindings.Add("Text", DataSource, "SoTT");
            celTenBietDuoc_CT.DataBindings.Add("Text", DataSource, "TenDV");
            celNuocSX_CT.DataBindings.Add("Text", DataSource, "NuocSX");
            celNongDo_CT.DataBindings.Add("Text", DataSource, "HamLuong");
            celDonVi_CT.DataBindings.Add("Text", DataSource, "DonVi");
            celDuongDung_CT.DataBindings.Add("Text", DataSource, "DuongD");
            celSoLuong_CT.DataBindings.Add("Text", DataSource, "SoLuong");
            celDonGia_CT.DataBindings.Add("Text", DataSource, "DonGia");

            GroupHeader1.GroupFields.Add(new GroupField("TenHC"));
            GroupHeader1.GroupFields.Add(new GroupField("MaATC"));
            GroupHeader1.GroupFields.Add(new GroupField("SoTT"));
        }
        int count = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            //count = 1;
            GroupHeader1.Visible = false;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (count == 1)
            {
                string tenhc = "";
                if (this.GetCurrentColumnValue("TenHC") != null)
                {
                    tenhc = this.GetCurrentColumnValue("TenHC").ToString();
                    celTenHC_CT.Text = tenhc;
                }
                else
                    celTenHC_CT.Text = "";
                string maatc = "";
                if (this.GetCurrentColumnValue("MaATC") != null)
                {
                    maatc = this.GetCurrentColumnValue("MaATC").ToString();
                    celMaATC_CT.Text = maatc;
                }
                else
                    celMaATC_CT.Text = "";
                string ttbd = "";
                if (this.GetCurrentColumnValue("SoTT") != null)
                {
                    ttbd = this.GetCurrentColumnValue("SoTT").ToString();
                    celTTBietDuoc_CT.Text = ttbd;
                }
                else
                    celTTBietDuoc_CT.Text = "";
            }
            else
            {
                celMaATC_CT.Text = "";
                celTenHC_CT.Text = "";
                celTTBietDuoc_CT.Text = "";
            }
            count++;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            count = 1;
        }
    }
}
