using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repSoKiemNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public repSoKiemNhap()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuocGh2.DataBindings.Add("Text", DataSource, "TenNhomDuoc");
            colTenDuocGh1.DataBindings.Add("Text", DataSource, "TenTieuNhomDuoc");
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");

            colSoCT.DataBindings.Add("Text", DataSource, "SoCT");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoKS.DataBindings.Add("Text", DataSource, "SoKS");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colHanDung.DataBindings.Add("Text", DataSource, "HanDung");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];

           // colSoLuongGh2.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
           // colSoLuongGh1.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[1];

           // colThanhTienGh2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString =DungChung.Bien.FormatString[1];
           // colThanhTienGh1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            colThanhTienTong.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader2.GroupFields.Add(new GroupField("TenNhomDuoc"));
            GroupHeader1.GroupFields.Add(new GroupField("TenTieuNhomDuoc"));
        }
        int sttGh2=1;
        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {
            //switch (sttGh2) {
            //    case 1:
            //        colSTTGh2.Text = "A";
            //        break;
            //    case 2:
            //        colSTTGh2.Text = "B";
            //        break;
            //    case 3:
            //        colSTTGh2.Text = "C";
            //        break;
            //    case 4:
            //        colSTTGh2.Text = "D";
            //        break;
            //    case 5:
            //        colSTTGh2.Text = "E";
            //        break;

            //}
            //sttGh2++;
        }
        int sttGh1 = 1;
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            //switch (sttGh1)
            //{
            //    case 1:
            //        colSTTGh1.Text = "I";
            //        break;
            //    case 2:
            //        colSTTGh1.Text = "II";
            //        break;
            //    case 3:
            //        colSTTGh1.Text = "III";
            //        break;
            //    case 4:
            //        colSTTGh1.Text = "IV";
            //        break;
            //    case 5:
            //        colSTTGh1.Text = "IV";
            //        break;

            //}
            //sttGh1++;
        }
        private void colHanDung_BeforePrint(object sender, CancelEventArgs e)
        {
            if (GetCurrentColumnValue("HanDung") != null && GetCurrentColumnValue("HanDung").ToString().Length >= 10)
            {
                colHanDung.Text = colHanDung.Text.Substring(0, 10);
            }
            else colHanDung.Text = "";
        }

        private void repSoKiemNhap_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQCQ.Value = DungChung.Bien.TenCQCQ.ToUpper();
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
                xrTable10.Visible = false;
            else
                xrTable10.Visible = true;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27023")
                SubBand1.Visible = false;
            else
                SubBand1.Visible = true;
        }

        

        
       
    }
}
