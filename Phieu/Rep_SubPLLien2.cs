using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_SubPLLien2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_SubPLLien2()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colDVT.DataBindings.Add("Text", DataSource, "DonVi");
            colTenDv.DataBindings.Add("Text", DataSource, "TenDV");
            colThanhtien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString=DungChung.Bien.FormatString[1];
            colSL.DataBindings.Add("Text", DataSource, "SL").FormatString = DungChung.Bien.FormatString[0];
            colTP.DataBindings.Add("Text", DataSource, "SL").FormatString = DungChung.Bien.FormatString[0];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            //txtKhoa.Text=
        }

    }
}
