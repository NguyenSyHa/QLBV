using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_CanhBaoThuocSLHDmin : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_CanhBaoThuocSLHDmin()
        {
            InitializeComponent();
        }
        public void Bind()
        {
            //CellTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            CellTenThuoc.DataBindings.Add("Text", DataSource, "tenthuoc");
            CellNgayNhap.DataBindings.Add("Text", DataSource, "ngaynhap").FormatString="{0:dd/MM/yyyy}";
            CellHanDung.DataBindings.Add("Text", DataSource, "handung").FormatString="{0:dd/MM/yyyy}";
            CellNgayHT.DataBindings.Add("Text", DataSource, "ngayht");
            CellSLTon.DataBindings.Add("Text", DataSource, "slton");
            CellSLMin.DataBindings.Add("Text", DataSource, "slmin");
            CellBaoSL.DataBindings.Add("Text", DataSource, "baosl");
            CellBaoHanDung.DataBindings.Add("Text", DataSource, "baohd");
           // CellGhiChu.DataBindings.Add("Text", DataSource, "madv");
        }

    }
}
