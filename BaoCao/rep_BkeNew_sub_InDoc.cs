using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BkeNew_sub_InDoc : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BkeNew_sub_InDoc()
        {
            InitializeComponent();
        }
        public void Bindingdata()
        {
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celSoLuong.DataBindings.Add("Text", DataSource, "SoLuong").FormatString = DungChung.Bien.FormatString[0];
            celDonGiaBV.DataBindings.Add("Text", DataSource, "DonGiaBV").FormatString = DungChung.Bien.FormatString[1];
            celDonGiaBH.DataBindings.Add("Text", DataSource, "DonGiaBH").FormatString = DungChung.Bien.FormatString[1];
            celTyLeDV.DataBindings.Add("Text", DataSource, "TyLeTTDV");

            celThanhTienBV.DataBindings.Add("Text", DataSource, "ThanhTienBV").FormatString = DungChung.Bien.FormatString[1];
            celThanhTianBV_T.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celThanhTianBV_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBV_G2.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celTTBV_G2.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBV_G1.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celTTBV_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBV_G0.DataBindings.Add("Text", DataSource, "ThanhTienBV");
            celTTBV_G0.Summary.FormatString = DungChung.Bien.FormatString[1];

            celThanhTienBH.DataBindings.Add("Text", DataSource, "ThanhTienBH").FormatString = DungChung.Bien.FormatString[1];
            celThanhTienBH_T.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celThanhTienBH_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBH_G.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celTTBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBH_G1.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celTTBH_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTTBH_G0.DataBindings.Add("Text", DataSource, "ThanhTienBH");
            celTTBH_G0.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTyLeBH.DataBindings.Add("Text", DataSource, "TyLeTTBH");
            celTienBH.DataBindings.Add("Text", DataSource, "TienBH").FormatString = DungChung.Bien.FormatString[1];
            celTienBH_T.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G1.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBH_G0.DataBindings.Add("Text", DataSource, "TienBH");
            celTienBH_G0.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTienBN.DataBindings.Add("Text", DataSource, "TienBN").FormatString = DungChung.Bien.FormatString[1];
            celTienBN_T.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBN_G.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBN_G1.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienBN_G0.DataBindings.Add("Text", DataSource, "TienBN");
            celTienBN_G0.Summary.FormatString = DungChung.Bien.FormatString[1];

            celTienNGDM.DataBindings.Add("Text", DataSource, "TienNgBH").FormatString = DungChung.Bien.FormatString[1];
            celTienNGDM_T.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienNGDM_T.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienNGDM_G.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienNGDM_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienNGDM_G1.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienNGDM_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTienNGDM_G0.DataBindings.Add("Text", DataSource, "TienNgBH");
            celTienNGDM_G0.Summary.FormatString = DungChung.Bien.FormatString[1];

            coltienkhac.DataBindings.Add("Text", DataSource, "TienKhac").FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_R.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_R.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_G.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_G.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_G1.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_G1.Summary.FormatString = DungChung.Bien.FormatString[1];
            coltienkhac_G0.DataBindings.Add("Text", DataSource, "TienKhac");
            coltienkhac_G0.Summary.FormatString = DungChung.Bien.FormatString[1];

            celNhom.DataBindings.Add("Text", DataSource, "TenNhom");
            celTieuNhom.DataBindings.Add("Text", DataSource, "TenTieuNhom");
            celDongY.DataBindings.Add("Text", DataSource, "TenDongTayY");

            GroupHeader2.GroupFields.Add(new GroupField("STTNhom"));
            GroupHeader1.GroupFields.Add(new GroupField("STTTieuN"));
            if (DungChung.Bien.MaBV == "14017")
            {
                GroupHeader3.Visible = true;
                GroupHeader3.GroupFields.Add(new GroupField("TenDongTayY"));
            }
        }

        private void GroupHeader3_BeforePrint(object sender, CancelEventArgs e)
        {
            var tenDongY = this.GetCurrentColumnValue("TenDongTayY");
            if (tenDongY == null || string.IsNullOrWhiteSpace(this.GetCurrentColumnValue("TenDongTayY").ToString()) || (this.GetCurrentColumnValue("TenDongTayY").ToString() != "Thuốc đông y" && this.GetCurrentColumnValue("TenDongTayY").ToString() != "Thuốc tây y"))
                e.Cancel = true;
        }
    }
}
