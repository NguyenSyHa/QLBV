using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.FormThamSo
{
    public partial class rep_NXT_SanXuatThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_NXT_SanXuatThuoc()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celMa.DataBindings.Add("Text", DataSource, "MaTam");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            cel4.DataBindings.Add("Text", DataSource, "TonDKSL").FormatString = DungChung.Bien.FormatString[0];
            cel5.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            cel6.DataBindings.Add("Text", DataSource, "NhapTKSL").FormatString = DungChung.Bien.FormatString[0];
            cel7.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            cel8.DataBindings.Add("Text", DataSource, "NhapKhac_SL").FormatString = DungChung.Bien.FormatString[0];
            cel9.DataBindings.Add("Text", DataSource, "NhapKhac_TT").FormatString = DungChung.Bien.FormatString[1];
            cel10.DataBindings.Add("Text", DataSource, "TongXuatTKSL").FormatString = DungChung.Bien.FormatString[0];
            cel11.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
            cel12.DataBindings.Add("Text", DataSource, "XuatKhac_SL").FormatString = DungChung.Bien.FormatString[0];
            cel13.DataBindings.Add("Text", DataSource, "XuatKhac_TT").FormatString = DungChung.Bien.FormatString[1];
            cel14.DataBindings.Add("Text", DataSource, "TonCKSL").FormatString = DungChung.Bien.FormatString[0];
            cel15.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            cel16.DataBindings.Add("Text", DataSource, "ConKK_SL").FormatString = DungChung.Bien.FormatString[0];
            cel17.DataBindings.Add("Text", DataSource, "ConKK_TT").FormatString = DungChung.Bien.FormatString[1];
            cel18.DataBindings.Add("Text", DataSource, "Thua_SL").FormatString = DungChung.Bien.FormatString[0];
            cel19.DataBindings.Add("Text", DataSource, "Thua_TT").FormatString = DungChung.Bien.FormatString[1];
            cel20.DataBindings.Add("Text", DataSource, "Thieu_SL").FormatString = DungChung.Bien.FormatString[0];
            cel21.DataBindings.Add("Text", DataSource, "Thieu_TT").FormatString = DungChung.Bien.FormatString[1];


            celTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            cel5_G.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];          
            cel7_G.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];          
            cel9_G.DataBindings.Add("Text", DataSource, "NhapKhac_TT").FormatString = DungChung.Bien.FormatString[1];           
            cel11_G.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];            
            cel13_G.DataBindings.Add("Text", DataSource, "XuatKhac_TT").FormatString = DungChung.Bien.FormatString[1];          
            cel15_G.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];          
            cel17_G.DataBindings.Add("Text", DataSource, "ConKK_TT").FormatString = DungChung.Bien.FormatString[1];          
            cel19_G.DataBindings.Add("Text", DataSource, "Thua_TT").FormatString = DungChung.Bien.FormatString[1];           
            cel21_G.DataBindings.Add("Text", DataSource, "Thieu_TT").FormatString = DungChung.Bien.FormatString[1];


            cel5_T.DataBindings.Add("Text", DataSource, "TonDKTT").FormatString = DungChung.Bien.FormatString[1];
            cel7_T.DataBindings.Add("Text", DataSource, "NhapTKTT").FormatString = DungChung.Bien.FormatString[1];
            cel9_T.DataBindings.Add("Text", DataSource, "NhapKhac_TT").FormatString = DungChung.Bien.FormatString[1];
            cel11_T.DataBindings.Add("Text", DataSource, "TongXuatTKTT").FormatString = DungChung.Bien.FormatString[1];
            cel13_T.DataBindings.Add("Text", DataSource, "XuatKhac_TT").FormatString = DungChung.Bien.FormatString[1];
            cel15_T.DataBindings.Add("Text", DataSource, "TonCKTT").FormatString = DungChung.Bien.FormatString[1];
            cel17_T.DataBindings.Add("Text", DataSource, "ConKK_TT").FormatString = DungChung.Bien.FormatString[1];
            cel19_T.DataBindings.Add("Text", DataSource, "Thua_TT").FormatString = DungChung.Bien.FormatString[1];
            cel21_T.DataBindings.Add("Text", DataSource, "Thieu_TT").FormatString = DungChung.Bien.FormatString[1];

            cel5_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel7_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel9_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel11_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel13_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel15_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel17_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel19_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel21_G.Summary.FormatString = DungChung.Bien.FormatString[1];


            cel5_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel7_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel9_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel11_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel13_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel15_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel17_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel19_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            cel21_T.Summary.FormatString = DungChung.Bien.FormatString[1];

            lblIDNhom.DataBindings.Add("Text", DataSource, "IdNhom");
            GroupHeader2.GroupFields.Add(new GroupField("IdNhom"));
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ;
            celCQCQ.Text = DungChung.Bien.TenCQCQ;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27021")
            {
                titGiamDoc.Text = "GIÁM ĐỐC";
                celGiamDoc.Text = DungChung.Bien.GiamDoc;
                titKhoaDuoc.Text = "KHOA DƯỢC";
                celKhoaDuoc.Text = DungChung.Bien.TruongKhoaDuoc;
                titKeToan.Text = "KẾ TOÁN";
                celKeToan.Text = DungChung.Bien.KeToanTruong;
                titThuKho.Text = "THỦ KHO";
                celThuKho.Text = DungChung.Bien.ThuKho;
            }
        }
    }
}
