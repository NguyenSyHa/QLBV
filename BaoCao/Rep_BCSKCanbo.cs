using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCSKCanbo : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCSKCanbo()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel1.Text = DungChung.Bien.TenCQCQ;
            xrLabel2.Text = DungChung.Bien.TenCQ;
        }
        public void BindingData()
        {
            xrTableCell15.DataBindings.Add("Text", DataSource, "_Hoten");
            xrTableCell12.DataBindings.Add("Text", DataSource, "_Tuoi");
            xrTableCell17.DataBindings.Add("Text", DataSource, "_Capbac");
            xrTableCell18.DataBindings.Add("Text", DataSource, "_Chucvu");
            xrTableCell13.DataBindings.Add("Text", DataSource, "_Songayom");
            xrTableCell19.DataBindings.Add("Text", DataSource, "_Songaynamvien");
            xrTableCell20.DataBindings.Add("Text", DataSource, "_Chandoan");
            xrTableCell21.DataBindings.Add("Text", DataSource, "_Mabenh");
            xrTableCell22.DataBindings.Add("Text", DataSource, "_Chuyenvien");
            xrTableCell14.DataBindings.Add("Text", DataSource, "_Ghichu");
        }

    }
}
