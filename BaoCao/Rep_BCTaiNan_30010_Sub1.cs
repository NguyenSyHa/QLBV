using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BCTaiNan_30010_Sub1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_BCTaiNan_30010_Sub1()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            celmaCSKCB.DataBindings.Add("Text", DataSource, "MaCSKCB");
            celTong.DataBindings.Add("Text", DataSource, "TongSo");
            celkhoi.DataBindings.Add("Text", DataSource, "Khoi");
            celdogiam.DataBindings.Add("Text", DataSource, "DoGiam");
            celkodoi.DataBindings.Add("Text", DataSource, "KoDoi");
            celnanghon.DataBindings.Add("Text", DataSource, "NangHon");
            celtuvong.DataBindings.Add("Text", DataSource, "TuVong");

            celtongso.DataBindings.Add("Text", DataSource, "TongSo");
            celtongkhoi.DataBindings.Add("Text", DataSource, "Khoi");
            celtongdogiam.DataBindings.Add("Text", DataSource, "DoGiam");
            celtongkodoi.DataBindings.Add("Text", DataSource, "KoDoi");
            celtongnanghon.DataBindings.Add("Text", DataSource, "NangHon");
            celtongtuvong.DataBindings.Add("Text", DataSource, "TuVong");
        }
    }
}
