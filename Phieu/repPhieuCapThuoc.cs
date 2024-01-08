using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuCapThuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuCapThuoc()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
        public void BindData()
        {
            cell_TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            cell_SoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = "{0:##,###}";
            cellDVT.DataBindings.Add("Text", DataSource, "DonVi");
            cell_HD.DataBindings.Add("Text", DataSource, "GhiChu");

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ;
        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {


        }

    }
}
