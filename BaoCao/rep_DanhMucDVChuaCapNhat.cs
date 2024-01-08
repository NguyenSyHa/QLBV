using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DanhMucDVChuaCapNhat : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DanhMucDVChuaCapNhat()
        {
            InitializeComponent();
        }
        public void bindingdata()
        {
            colMaQD.DataBindings.Add("Text", DataSource, "MaDV") ;
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonViTinh.DataBindings.Add("Text", DataSource, "DonVi");
            colDonGia.DataBindings.Add("Text", DataSource, "DonGia").FormatString = DungChung.Bien.FormatString[0];
        }
    }
}
