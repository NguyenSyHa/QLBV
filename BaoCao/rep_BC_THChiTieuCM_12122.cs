using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_THChiTieuCM_12122 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_THChiTieuCM_12122()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            celNguoiLapBieu.Text = DungChung.Bien.NguoiLapBieu;
            celGiamDoc.Text = DungChung.Bien.GiamDoc;
        }

        public void BindingData()
        {
            celStt.DataBindings.Add("Text", DataSource, "Stt");
            celND.DataBindings.Add("Text", DataSource, "ChiTieu");
            celDV.DataBindings.Add("Text", DataSource, "DonVi");
            celChiTieuNam.DataBindings.Add("Text", DataSource, "ChiTieuNam");
            celTHNam.DataBindings.Add("Text", DataSource, "THNam");
            celTyLeNam.DataBindings.Add("Text", DataSource, "TyLeNam");
            celTHQuy1.DataBindings.Add("Text", DataSource, "THQuy1");
            celTyLeQuy1.DataBindings.Add("Text", DataSource, "TyLeQuy1");
            celTHQuy2.DataBindings.Add("Text", DataSource, "THQuy2");
            celTyLeQuy2.DataBindings.Add("Text", DataSource, "TyLeQuy2");
            celTHQuy3.DataBindings.Add("Text", DataSource, "THQuy3");
            celTyLeQuy3.DataBindings.Add("Text", DataSource, "TyLeQuy3");
            celTHQuy4.DataBindings.Add("Text", DataSource, "THQuy4");
            celTyLeQuy4.DataBindings.Add("Text", DataSource, "TyLeQuy4");
        }
    }
}
