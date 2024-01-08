using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class RepBTKThuoc_VTTHPhongMo_ : DevExpress.XtraReports.UI.XtraReport
    {
        public RepBTKThuoc_VTTHPhongMo_()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        public void BindingData()
        {
            celTenThuoc.DataBindings.Add("Text", DataSource, "Thuoc");
            celDVTthuoc.DataBindings.Add("Text", DataSource, "DvtThuoc");
            celSLThuoc.DataBindings.Add("Text", DataSource, "SlThuoc");
            celsttThuoc.DataBindings.Add("Text", DataSource, "SttThuoc");

            celVTTH.DataBindings.Add("Text", DataSource, "Vtth");
            celDVTVTTH.DataBindings.Add("Text", DataSource, "DvtVTTH");
            celSLVTTH.DataBindings.Add("Text", DataSource, "SlVTTH");
            celsttVTTH.DataBindings.Add("Text", DataSource, "SttVTYT");
        }

        private void xrTable2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (celsttThuoc.Text == "0")
                celsttThuoc.Text = null;
            if (celsttVTTH.Text == "0")
                celsttVTTH.Text = null;
            if (celSLThuoc.Text == "0")
                celSLThuoc.Text = null;
            if (celSLVTTH.Text == "0")
                celSLVTTH.Text = null;

        }

    }
}
