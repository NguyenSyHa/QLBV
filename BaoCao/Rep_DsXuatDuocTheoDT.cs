using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class Rep_DsXuatDuocTheoDT : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_DsXuatDuocTheoDT()
        {
            InitializeComponent();
          
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public void BindingData()
        {
            colTenbn.DataBindings.Add("Text", DataSource, "TenBN");
            col1.DataBindings.Add("Text", DataSource, "TE").FormatString = DungChung.Bien.FormatString[1];
            col2.DataBindings.Add("Text", DataSource, "GD").FormatString = DungChung.Bien.FormatString[1];
            col3.DataBindings.Add("Text", DataSource, "HS").FormatString = DungChung.Bien.FormatString[1];
            col4.DataBindings.Add("Text", DataSource, "HNDTDK").FormatString = DungChung.Bien.FormatString[1];
            col5.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            col6.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            col7.DataBindings.Add("Text", DataSource, "BN").FormatString = DungChung.Bien.FormatString[1];
            col8.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];

            col1t.DataBindings.Add("Text", DataSource, "TE").FormatString = DungChung.Bien.FormatString[1];
            col2t.DataBindings.Add("Text", DataSource, "GD").FormatString = DungChung.Bien.FormatString[1];
            col3t.DataBindings.Add("Text", DataSource, "HS").FormatString = DungChung.Bien.FormatString[1];
            col4t.DataBindings.Add("Text", DataSource, "HNDTDK").FormatString = DungChung.Bien.FormatString[1];
            col5t.DataBindings.Add("Text", DataSource, "Khac").FormatString = DungChung.Bien.FormatString[1];
            col6t.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
            col7t.DataBindings.Add("Text", DataSource, "BN").FormatString = DungChung.Bien.FormatString[1];
            col8t.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtDiaChi.Text = DungChung.Bien.DiaChi;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoilapBieu.Text = DungChung.Bien.NguoiLapBieu;
        }
       
    }
}
