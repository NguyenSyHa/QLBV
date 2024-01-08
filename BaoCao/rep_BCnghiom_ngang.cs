using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCnghiom_ngang : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCnghiom_ngang()
        {
            InitializeComponent();
        }
        public void databinding()
        {
            col_tenbn.DataBindings.Add("Text", DataSource, "TenBNhan");
            col_ngaysinh.DataBindings.Add("Text", DataSource, "NgaySinh");
            if(DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                col_soseri.DataBindings.Add("Text", DataSource, "SoNghiOm");
            else
                col_soseri.DataBindings.Add("Text", DataSource, "MaBNhan");

            col_sthe.DataBindings.Add("Text", DataSource, "SThe");
            col_dv.DataBindings.Add("Text", DataSource, "NoiLV");
            col_chuandoan.DataBindings.Add("Text", DataSource, "GhiChu");
            col_songay.DataBindings.Add("Text", DataSource, "songay").FormatString = DungChung.Bien.FormatString[1];
            col_tungay.DataBindings.Add("Text", DataSource, "NgayNghi").FormatString= "{0:dd/MM/yyyy}";
            col_denngay.DataBindings.Add("Text", DataSource, "NgayHen").FormatString = "{0:dd/MM/yyyy}";
            col_kphong.DataBindings.Add("Text", DataSource, "TenKP");
            col_bacsi.DataBindings.Add("Text", DataSource, "TenCB");
        }
    }
}
