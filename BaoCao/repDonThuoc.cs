using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDonThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public repDonThuoc()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        public void BindData() {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");
            
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
            if (DungChung.Bien.MaBV == "24009" )
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009") {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
                xrTableCell7.Visible = true;
            }

        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV=="26007" )
            {
                xrThuKho.Visible = true;
                xrThuKhoky.Visible = true;
                xrThuKhoNguoi.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
                xrTenBS.Visible = false;
            if (DungChung.Bien.MaBV == "30009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;
                xrTableRow7.Visible = false;
            }
            
        }

    }
}
