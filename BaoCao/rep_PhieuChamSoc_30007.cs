using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuChamSoc_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuChamSoc_30007()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "Ngay");
            //celNgay1.DataBindings.Add("Text", DataSource, "Ngay");
            celylenh.DataBindings.Add("Text", DataSource, "DuongDTri");
            //celylenh1.DataBindings.Add("Text", DataSource, "DuongDTri");
            celDienbien.DataBindings.Add("Text", DataSource, "YLenh");
            //celDienbien1.DataBindings.Add("Text", DataSource, "YLenh");
            celTenCB.DataBindings.Add("Text", DataSource, "TenCB");
            //celTenCB1.DataBindings.Add("Text", DataSource, "TenCB");
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            //{
            //    SubBand3.Visible = true;
            //    SubBand4.Visible = true;
            //}
            //else
            //{
            //    SubBand2.Visible = true;
            //    SubBand1.Visible = true;
            //}
        }
    }
}
