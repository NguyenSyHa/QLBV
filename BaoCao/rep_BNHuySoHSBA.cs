using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BNHuySoHSBA : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BNHuySoHSBA()
        {
            InitializeComponent();
        }
        public void Bind()
        {
            //colTenBNhan.DataBindings.Add("Text", DataSource, "hoTen");
            CellTenBN.DataBindings.Add("Text", DataSource, "TenBNhan");
            CellKhoaPhong.DataBindings.Add("Text", DataSource, "TenKP");
            CellDiaChi.DataBindings.Add("Text", DataSource, "DChi");
            CellSoKB.DataBindings.Add("Text", DataSource, "sokhambenh");
            CellMaYTe.DataBindings.Add("Text", DataSource, "mayte");
            CellSoPhieuCLS.DataBindings.Add("Text", DataSource, "sophieuCLS");
            CellMaLuuTru.DataBindings.Add("Text", DataSource, "maluutru");
            CellSoVV.DataBindings.Add("Text", DataSource, "sovvien");
            CellSoBA.DataBindings.Add("Text", DataSource, "soBA");
            CellSoRV.DataBindings.Add("Text", DataSource, "soravien");
            CellSoChuyenTuyen.DataBindings.Add("Text", DataSource, "sochuyenvien");

        }
    }
}
