using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuThuThuocNgoai_12345 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuThuThuocNgoai_12345()
        {
            InitializeComponent();
        }
        public void bindingdata()
        {
            coltendv.DataBindings.Add("Text", DataSource, "TenDV");
            colSoluong.DataBindings.Add("Text", DataSource, "SoLuongX");
            colDongia.DataBindings.Add("Text", DataSource, "DonGia");
            colDonvi.DataBindings.Add("Text", DataSource, "DonVi");
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
    }
}
