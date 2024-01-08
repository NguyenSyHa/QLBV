using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BC_XNKSDViKhuanLao : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BC_XNKSDViKhuanLao()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblDonVi.Text = "ĐƠN VỊ: " + DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            cel_Ngay.DataBindings.Add("Text", DataSource, "NgayTH");
            cel_SoKSD.DataBindings.Add("Text", DataSource, "SoKSD");
            cel_SoCay.DataBindings.Add("Text", DataSource, "SoCay");
            cel_TenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            cel_Tuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            cel_DiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            cel_H.DataBindings.Add("Text", DataSource, "H");
            cel_R.DataBindings.Add("Text", DataSource, "R");
            cel_S.DataBindings.Add("Text", DataSource, "S");
            cel_E.DataBindings.Add("Text", DataSource, "E");
            cel_PZA.DataBindings.Add("Text", DataSource, "PZA");
            cel_AK.DataBindings.Add("Text", DataSource, "AK");
            cel_OF.DataBindings.Add("Text", DataSource, "OF");
            cel_KM.DataBindings.Add("Text", DataSource, "KM");
            cel_CAP.DataBindings.Add("Text", DataSource, "CAP");
            cel_NgayTraKQ.DataBindings.Add("Text", DataSource, "NgayKQ");
            cel_NguoiLamXN.DataBindings.Add("Text", DataSource, "NguoiLamXN");
            cel_GhiChu.DataBindings.Add("Text", DataSource, "GhiChu");
        }
    }
}
