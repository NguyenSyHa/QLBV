using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_THSoLuotXN_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_THSoLuotXN_30005()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            cellTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            cellTenNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            cellTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celNgayCD.DataBindings.Add("Text", DataSource, "NgayCD").FormatString = "{0: dd/MM/yyyy}";
            celkp.DataBindings.Add("Text", DataSource, "TenKP");
            GroupHeader1.GroupFields.Add(new GroupField("NgayCD"));
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            labTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
