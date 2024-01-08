using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_CanBoHoTro : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_CanBoHoTro()
        {
            InitializeComponent();
        }
        public void DataBindding() {
            colTenNV.DataBindings.Add("Text", DataSource, "TenNV");
            colChucVu.DataBindings.Add("Text", DataSource, "ChucVu");
            colPhongBan.DataBindings.Add("Text", DataSource, "PhongBan");
            colSDT.DataBindings.Add("Text", DataSource, "SoDT1");
            colSDT2.DataBindings.Add("Text", DataSource, "SoDT2");
            colDiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            colBietDanh.DataBindings.Add("Text", DataSource, "BietDanh");
        }
    }
}
