using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_subPhieuNhap_12001_KhoLao : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_subPhieuNhap_12001_KhoLao()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDuoc.DataBindings.Add("Text", DataSource, "TenDuoc");
            colsolo.DataBindings.Add("Text", DataSource, "SoLo");
            //colHanDung1.DataBindings.Add("Text", DataSource, "HanDung").ToString();
            colnoisx.DataBindings.Add("Text", DataSource, "DonViTinh");
            colhandung.DataBindings.Add("Text", DataSource, "HanDung");
            coldonvi.DataBindings.Add("Text", DataSource, "DonViTinh");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            colThanhTien.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colsoluong.DataBindings.Add("Text", DataSource, "SoLuongN").FormatString = DungChung.Bien.FormatString[0];
            colthanhtien_T.DataBindings.Add("Text", DataSource, "ThanhTienN").FormatString = DungChung.Bien.FormatString[1];
            colthanhtien_T.Summary.FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
