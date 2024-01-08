using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCTHThuocLinhKhoa_TuTruc : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCTHThuocLinhKhoa_TuTruc()
        {
            InitializeComponent();
            cqcq.Text = DungChung.Bien.TenCQCQ;
            cq.Text = DungChung.Bien.TenCQ;
        }

        public void BindingData()
        {
            sopl.DataBindings.Add("Text", DataSource, "SoPL");
            ngaythang.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0:dd/MM/yyyy}";
            tenthuoc.DataBindings.Add("Text", DataSource, "TenDV");
            dvt.DataBindings.Add("Text", DataSource, "DonVi");
            dongia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            soluong.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1]; ;
            thanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1]; ;
            tongthanhtien.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
            
            tenthuoc1.DataBindings.Add("Text", DataSource, "TenDV");
            dvt1.DataBindings.Add("Text", DataSource, "DonVi");
            dongia1.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            soluong1.DataBindings.Add("Text", DataSource, "SoLuongX").FormatString = DungChung.Bien.FormatString[1]; ;
            thanhtien1.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1]; ;
            tongthanhtien1.DataBindings.Add("Text", DataSource, "ThanhTienX").FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
