using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repDonThuocGN_HTT : DevExpress.XtraReports.UI.XtraReport
    {
        public repDonThuocGN_HTT()
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
            if (DungChung.Bien.MaBV == "24009")
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }

        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009")
            {
                //txtbnky.Visible = false;
                //txttenBN.Visible = false;
            }
        }

    }
}
