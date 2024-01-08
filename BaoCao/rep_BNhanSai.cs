using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BNhanSai : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BNhanSai()
        {
            InitializeComponent();
        }

        public void DataBinding() {
            colMaBN.DataBindings.Add("Text", DataSource, "MaBNhan");
            colTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            colGTinh.DataBindings.Add("Text", DataSource, "GTinh");
            colSThe.DataBindings.Add("Text", DataSource, "SThe");
            colDTuong.DataBindings.Add("Text", DataSource, "DTuong");
            colMaICD.DataBindings.Add("Text", DataSource, "MaICD");
            colNgayvao.DataBindings.Add("Text", DataSource, "NgayVao").FormatString = "{0:dd/MM/yyyy}";
            colNgayra.DataBindings.Add("Text", DataSource, "NgayRa").FormatString = "{0:dd/MM/yyyy}"; 
            colNgayTT.DataBindings.Add("Text", DataSource, "NgayTT").FormatString = "{0:dd/MM/yyyy}";
            colGhiChu.DataBindings.Add("Text", DataSource, "DChi");
        }
    }
}
