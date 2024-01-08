using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCTaiNan_30010_Sub2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCTaiNan_30010_Sub2()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            celmann.DataBindings.Add("Text", DataSource, "MaNN");
            celTennn.DataBindings.Add("Text", DataSource, "TenNN");
            celtong.DataBindings.Add("Text", DataSource, "Tong");
            celtongso.DataBindings.Add("Text", DataSource, "Tong");
        }
    }
}
