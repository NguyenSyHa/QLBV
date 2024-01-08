using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_thongkecanbophongkham : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_thongkecanbophongkham()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            colGhichu.DataBindings.Add("Text", DataSource, "Ghichu");
            colSoluongtong.DataBindings.Add("Text", DataSource, "Soluottong");
            colSoluotchieu.DataBindings.Add("Text", DataSource, "Soluotchieu");
            colSoluotsang.DataBindings.Add("Text", DataSource, "Soluotsang");
            colSoluottong.DataBindings.Add("Text", DataSource, "Soluottong");
            colTenBS.DataBindings.Add("Text", DataSource, "TenBS");
            colTenPK.DataBindings.Add("Text", DataSource, "TenPK");
        }
    }
}
