using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCNXT_VAT : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCNXT_VAT()
        {
            InitializeComponent();
        }


        internal void BindingData()
        {

            celNgay.DataBindings.Add("Text", DataSource, "NgayNhap").FormatString = "{0: dd/MM}";
            celTonDK_SL.DataBindings.Add("Text", DataSource, "TonDK_SL").FormatString = DungChung.Bien.FormatString[0];
            celTonDK_TT.DataBindings.Add("Text", DataSource, "TonDK_TT").FormatString = DungChung.Bien.FormatString[1];
            celNhapHD_SL.DataBindings.Add("Text", DataSource, "NhapHD_SL").FormatString = DungChung.Bien.FormatString[0];
            celNhapHD_TT.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            celNhapKhac_SL.DataBindings.Add("Text", DataSource, "NhapKhac_SL").FormatString = DungChung.Bien.FormatString[0];
            celNhapKhac_TT.DataBindings.Add("Text", DataSource, "NhapKhac_TT").FormatString = DungChung.Bien.FormatString[1];
            celXuat_SL.DataBindings.Add("Text", DataSource, "Xuat_SL").FormatString = DungChung.Bien.FormatString[0];
            celXuat_TT.DataBindings.Add("Text", DataSource, "Xuat_TT").FormatString = DungChung.Bien.FormatString[1];
            celTonCK_SL.DataBindings.Add("Text", DataSource, "TonCK_SL").FormatString = DungChung.Bien.FormatString[0];
            celTonCK_TT.DataBindings.Add("Text", DataSource, "TonCK_TT").FormatString = DungChung.Bien.FormatString[1];

           
            celNhapHD_R.DataBindings.Add("Text", DataSource, "NhapHD_TT").FormatString = DungChung.Bien.FormatString[1];
            celNhapKhac_R.DataBindings.Add("Text", DataSource, "NhapKhac_TT").FormatString = DungChung.Bien.FormatString[1];
            celXuat_R.DataBindings.Add("Text", DataSource, "Xuat_TT").FormatString = DungChung.Bien.FormatString[1];
           
           
            celNhapHD_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celNhapKhac_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            celXuat_R.Summary.FormatString = DungChung.Bien.FormatString[1];
           
           
        }
    }
}
