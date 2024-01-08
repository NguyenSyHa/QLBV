using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCXetNghiem_30007_Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCXetNghiem_30007_Sub()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            colSoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
        }
    }
}
