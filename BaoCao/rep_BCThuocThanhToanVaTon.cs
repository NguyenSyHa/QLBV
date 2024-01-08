using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuocThanhToanVaTon : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuocThanhToanVaTon()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {
            celTenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celDonGia.DataBindings.Add("Text", DataSource, "DonGia");
            cel1SL.DataBindings.Add("Text", DataSource, "Ton1T_SL").FormatString = DungChung.Bien.FormatString[1];
            cel1_TT.DataBindings.Add("Text", DataSource, "Ton1T_TT").FormatString = DungChung.Bien.FormatString[1];
            celT_SL.DataBindings.Add("Text", DataSource, "TonTKChuaTT_SL").FormatString = DungChung.Bien.FormatString[1];
            celT_TT.DataBindings.Add("Text", DataSource, "TonTKChuaTT_TT").FormatString = DungChung.Bien.FormatString[1];
            celTT_SL.DataBindings.Add("Text", DataSource, "TonTKDaTT_SL").FormatString = DungChung.Bien.FormatString[1];
            celTT_TT.DataBindings.Add("Text", DataSource, "TonTKDaTT_TT").FormatString = DungChung.Bien.FormatString[1];
            celSL.DataBindings.Add("Text", DataSource, "SL").FormatString = DungChung.Bien.FormatString[1];
            celTT.DataBindings.Add("Text", DataSource, "TT").FormatString = DungChung.Bien.FormatString[1];
        }
    }
}
