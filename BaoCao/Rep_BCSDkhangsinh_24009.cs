using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCSDkhangsinh_24009 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCSDkhangsinh_24009()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
           // cel_TThoatchat.DataBindings.Add("Text", DataSource, "TThoatchat");
            cel_Tenhoatchat.DataBindings.Add("Text", DataSource, "TenHC");
            cel_MaATC.DataBindings.Add("Text", DataSource, "MaATC");
            cel_TTbietduoc.DataBindings.Add("Text", DataSource, "SoTT");
            cel_Tenbietduoc.DataBindings.Add("Text", DataSource, "TenDV");
            cel_Nuocsx.DataBindings.Add("Text", DataSource, "NuocSX");
            cel_Nongdo.DataBindings.Add("Text", DataSource, "HamLuong");
            cel_donvi.DataBindings.Add("Text", DataSource, "DonVi");
            cel_duongdung.DataBindings.Add("Text", DataSource, "DuongD");
            cel_Sl.DataBindings.Add("Text", DataSource, "SoLuong");
            cel_Dongia.DataBindings.Add("Text", DataSource, "DonGia");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lad_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lad_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            cel_giamdoc.Text = DungChung.Bien.GiamDoc;
            
        }

    }
}
