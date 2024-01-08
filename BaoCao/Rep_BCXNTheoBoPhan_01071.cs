using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCXNTheoBoPhan_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCXNTheoBoPhan_01071()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            coltendv.DataBindings.Add("Text", DataSource, "TenDV");
            colsoluong.DataBindings.Add("Text", DataSource, "SoLuong");
            coltentn.DataBindings.Add("Text", DataSource, "TenTN");
            coltongso.DataBindings.Add("Text", DataSource, "SoLuong");
            GroupHeader1.GroupFields.Add(new GroupField("IdTieuNhom"));
        }
    }
}
