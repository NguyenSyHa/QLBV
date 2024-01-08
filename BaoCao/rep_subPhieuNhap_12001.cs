using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_subPhieuNhap_12001 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_subPhieuNhap_12001()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
            colSoLo.DataBindings.Add("Text", DataSource, "MaQD");
            //colHanDung1.DataBindings.Add("Text", DataSource, "HanDung").ToString();
            colDVT.DataBindings.Add("Text", DataSource, "DonViTinh");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[1];
            colSLTN.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colSLCT.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            celtongtien.DataBindings.Add("Text", DataSource, "ThanhTienN");
            celtongtien.Summary.FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
