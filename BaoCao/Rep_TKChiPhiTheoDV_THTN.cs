using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_TKChiPhiTheoDV_THTN : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_TKChiPhiTheoDV_THTN()
        {
            InitializeComponent();
        }
        public void BinDingdata()
        {
            colTenTN.DataBindings.Add("Text", DataSource, "TenTN");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuongT").FormatString = DungChung.Bien.FormatString[0];
            colThanhTien.DataBindings.Add("Text", DataSource, "TTT").FormatString = DungChung.Bien.FormatString[1];

            colSoLuongT.DataBindings.Add("Text", DataSource, "SoLuongT");
            colSoLuongT.Summary.FormatString = DungChung.Bien.FormatString[0];
            colThanhTienT.DataBindings.Add("Text", DataSource, "TTT");
            colThanhTienT.Summary.FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
