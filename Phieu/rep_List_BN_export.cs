using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.Phieu
{
    public partial class rep_List_BN_export : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_List_BN_export()
        {
            InitializeComponent();
        }
        public void BinDingData(){
            col_Ten.DataBindings.Add("Text", DataSource, "Ho_ten");
            col_NS.DataBindings.Add("Text", DataSource, "Ngay_sinh");
            colSoThe.DataBindings.Add("Text", DataSource, "Ma_the");
            col_NgayRa.DataBindings.Add("Text", DataSource, "Ngay_ra").FormatString="{0:dd/MM/yyyy}";
            colTongTien.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString=DungChung.Bien.FormatString[1];
            col_Loi.DataBindings.Add("Text", DataSource, "Lydo_xt");
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txt_tenCQ.Text = DungChung.Bien.TenCQ;
            colNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
        }
    }
}
