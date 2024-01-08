using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_PLThuoctheothang_TT01 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PLThuoctheothang_TT01()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            celTendv.DataBindings.Add("Text", DataSource, "TenDV");
            celDonvi.DataBindings.Add("Text", DataSource, "DonVi");
            celSL.DataBindings.Add("Text", DataSource, "Soluong");
           
            //RFThanhTien.DataBindings.Add("Text", DataSource, "ThanhTien");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "27001")
                xrTableCell12.Text = "BÁC SỸ KÊ ĐƠN";
        }

        private void Donvi_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

    }
}
