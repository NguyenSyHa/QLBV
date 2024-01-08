using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_BNVaoVienNgoaiTru_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_BNVaoVienNgoaiTru_30007()
        {
            InitializeComponent();
        }

        public void Bindingdata()
        {
            gr_GroupDT.DataBindings.Add("Text", DataSource, "DTBN");
            //cel_DoiTuong.DataBindings.Add("Text", DataSource, "DTBN");
            cel_Khoa.DataBindings.Add("Text", DataSource, "TenKP");
            cel_TenBN.DataBindings.Add("Text", DataSource, "TenBN");
            cel_Nam.DataBindings.Add("Text", DataSource, "Nam");
            cel_Nu.DataBindings.Add("Text", DataSource, "Nu");
            cel_Tuoi.DataBindings.Add("Text", DataSource, "Tuoi");
            cel_DiaChi.DataBindings.Add("Text", DataSource, "DiaChi");
            cel_ChuanDoan.DataBindings.Add("Text", DataSource, "ChuanDoan");
            GroupHeader1.GroupFields.Add(new GroupField("DTBN"));
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void xrTable4_BeforePrint(object sender, CancelEventArgs e)
        {
            celNguoiLap.Text = DungChung.Bien.NguoiLapBieu;
            celGD.Text = DungChung.Bien.GiamDoc;
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lblTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lblTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
        }
    }
}
