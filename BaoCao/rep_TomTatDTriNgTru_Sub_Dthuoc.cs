using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_TomTatDTriNgTru_Sub_Dthuoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_TomTatDTriNgTru_Sub_Dthuoc()
        {
            InitializeComponent();
        }
        public void BindData()
        {
            TenDV.DataBindings.Add("Text", DataSource, "TenDV");
            SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
            DonVi.DataBindings.Add("Text", DataSource, "DonVi");
            HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");

        }
    }
}
