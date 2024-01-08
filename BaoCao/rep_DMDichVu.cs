using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_DMDichVu : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_DMDichVu()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            colTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            colMaDV.DataBindings.Add("Text", DataSource, "MaDV");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            colSLTon.DataBindings.Add("Text", DataSource, "SLT").FormatString = DungChung.Bien.FormatString[1]; 
            colSLMin.DataBindings.Add("Text", DataSource, "SLMin").FormatString = DungChung.Bien.FormatString[1]; 
            colChenLech.DataBindings.Add("Text", DataSource, "CL").FormatString = DungChung.Bien.FormatString[1];
            GroupHeader1.GroupFields.Add(new GroupField("TenNhom"));
        }

        int num = 0;
        private void colSTTNhom_BeforePrint(object sender, CancelEventArgs e)
        {
            num++;
            colSTTNhom.Text = num.ToString();
        }
    }
}
