using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class Rep_BcSuDungDuoc_BG_TT : DevExpress.XtraReports.UI.XtraReport
    {
        private bool ckHienThiTN =  false;

        public Rep_BcSuDungDuoc_BG_TT()
        {
            InitializeComponent();
        }

        public Rep_BcSuDungDuoc_BG_TT(bool htTieuNhom)
        {
            // TODO: Complete member initialization
            this.ckHienThiTN = htTieuNhom;
            InitializeComponent();
        }
        public void BindingData()
        {
            colNhomDVG2.DataBindings.Add("Text", DataSource, "NhomDV");
            if(ckHienThiTN)
            colTNDV.DataBindings.Add("Text", DataSource, "TenTN");
            colTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonGia.DataBindings.Add("Text", DataSource, "DongGia");
            colNuocSX.DataBindings.Add("Text", DataSource, "NuocSX");
            colDVT.DataBindings.Add("Text", DataSource, "DVT");
            colKP1.DataBindings.Add("Text", DataSource, "SL1").FormatString = DungChung.Bien.FormatString[1];
            colKP2.DataBindings.Add("Text", DataSource, "SL2").FormatString = DungChung.Bien.FormatString[1];
            colKP3.DataBindings.Add("Text", DataSource, "SL3").FormatString = DungChung.Bien.FormatString[1];
            colKP4.DataBindings.Add("Text", DataSource, "SL4").FormatString = DungChung.Bien.FormatString[1];
            colKP5.DataBindings.Add("Text", DataSource, "SL5").FormatString = DungChung.Bien.FormatString[1];
            colKP6.DataBindings.Add("Text", DataSource, "SL6").FormatString = DungChung.Bien.FormatString[1];
            colKP7.DataBindings.Add("Text", DataSource, "SL7").FormatString = DungChung.Bien.FormatString[1];
            colKP8.DataBindings.Add("Text", DataSource, "SL8").FormatString = DungChung.Bien.FormatString[1];
            colKP9.DataBindings.Add("Text", DataSource, "SL9").FormatString = DungChung.Bien.FormatString[1];
            colKP10.DataBindings.Add("Text", DataSource, "SL10").FormatString = DungChung.Bien.FormatString[1];
            colKP11.DataBindings.Add("Text", DataSource, "Sl11").FormatString = DungChung.Bien.FormatString[1];
            colKP12.DataBindings.Add("Text", DataSource, "SL12").FormatString = DungChung.Bien.FormatString[1];
            colKP13.DataBindings.Add("Text", DataSource, "SL13").FormatString = DungChung.Bien.FormatString[1];
            colKP14.DataBindings.Add("Text", DataSource, "SL14").FormatString = DungChung.Bien.FormatString[1];
            colKP15.DataBindings.Add("Text", DataSource, "SL15").FormatString = DungChung.Bien.FormatString[1];
            colKP16.DataBindings.Add("Text", DataSource, "SL16").FormatString = DungChung.Bien.FormatString[1];
            colKPTong.DataBindings.Add("Text", DataSource, "TC").FormatString = DungChung.Bien.FormatString[1];
            colThanhtien.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            
            cellSumGr1.Summary.FormatString = DungChung.Bien.FormatString[1];
            cellSumGr2.Summary.FormatString = DungChung.Bien.FormatString[1];
            cellSumGr2.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            cellSumGr1.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            
            //celabc.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            //celabc.Summary.FormatString = DungChung.Bien.FormatString[1];
            cellSumFooter.Summary.FormatString = DungChung.Bien.FormatString[1];
            cellSumFooter.DataBindings.Add("Text", DataSource, "ThanhTien").FormatString = DungChung.Bien.FormatString[1];
            txtTongtienTong.Text = "Tổng cộng tiền";
            
            GroupHeader1.GroupFields.Add(new GroupField("TenTN"));
            GroupHeader2.GroupFields.Add(new GroupField("NhomDV"));   
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            string nh = NhomDV.Value.ToString();
            if (nh != "0")
            {
                GroupHeader1.Visible = false;
            }

        }
        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            GroupHeader1.Visible = ckHienThiTN;
        }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTongtien1.Text = "Tổng tiền " + this.GetCurrentColumnValue("TenTN");
        }

        private void GroupHeader2_BeforePrint(object sender, CancelEventArgs e)
        {

            GroupHeader2.Visible = ckHienThiTN;
        }

        private void GroupFooter2_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTongtien2.Text = "Tổng tiền " + this.GetCurrentColumnValue("NhomDV");
        }
    }
}
