using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuChamSoc_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuChamSoc_20001()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celNgay.DataBindings.Add("Text", DataSource, "Ngay");
            celgio.DataBindings.Add("Text", DataSource, "Gio");
            celDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
            celylenh.DataBindings.Add("Text", DataSource, "YLenh");
            celTenCBTH.DataBindings.Add("Text", DataSource, "TenCB");
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            //txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    if (dem % 2 == 0)
            //    {
            //        SubBand1.Visible = true;
            //        SubBand2.Visible = false;
            //    }
            //    else
            //    {
            //        SubBand2.Visible = true;
            //        SubBand1.Visible = false;
            //    }
            //    dem++;
            //}
        }

    }
}
