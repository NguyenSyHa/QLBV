using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuThuThuocNgoai : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuThuThuocNgoai()
        {
            InitializeComponent();
        }
        public void bindingdata()
        {
            coltendv.DataBindings.Add("Text", DataSource, "TenDV");
            colchiphi.DataBindings.Add("Text", DataSource, "ThanhTienX");
            coltienbn.DataBindings.Add("Text", DataSource, "ThanhTienX");

            colcpt.DataBindings.Add("Text", DataSource, "ThanhTienX");
            colcpt.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltbnt.DataBindings.Add("Text", DataSource, "ThanhTienX");
            coltbnt.Summary.FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                txtNgay.Visible = false;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30372")
            {
                
                SubBand2.Visible = true;
                SubBand1.Visible = false;
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
        }
    }
}
