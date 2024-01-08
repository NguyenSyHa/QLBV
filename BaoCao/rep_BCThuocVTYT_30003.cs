using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuocVTYT_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuocVTYT_30003()
        {
            InitializeComponent();
        }

        string fomat = DungChung.Bien.FormatString[1];
        internal void BindingData()
        {
       
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            cel1.DataBindings.Add("Text", DataSource, "soBNRV").FormatString = DungChung.Bien.FormatString[0];
            cel2.DataBindings.Add("Text", DataSource, "ThuocTayTieuHao").FormatString = fomat;
            cel3.DataBindings.Add("Text", DataSource, "ThuocDongY").FormatString = fomat;
            cel4.DataBindings.Add("Text", DataSource, "TongThuocVTYT").FormatString = fomat;
            cel5.DataBindings.Add("Text", DataSource, "ThuocVTMien").FormatString = fomat;
            cel6.DataBindings.Add("Text", DataSource, "ThuocVTKMien").FormatString = fomat;
            cel7.DataBindings.Add("Text", DataSource, "TongThuocVTYT").FormatString = fomat;
            cel8.DataBindings.Add("Text", DataSource, "soBNMien").FormatString = DungChung.Bien.FormatString[0];
            cel9.DataBindings.Add("Text", DataSource, "TongDonMien").FormatString = fomat;


            cel1Th.DataBindings.Add("Text", DataSource, "soBNRV");
            cel1Th.Summary.FormatString = DungChung.Bien.FormatString[0];

            cel2Th.DataBindings.Add("Text", DataSource, "ThuocTayTieuHao");
            cel2Th.Summary.FormatString = fomat;

            cel3Th.DataBindings.Add("Text", DataSource, "ThuocDongY");
            cel3Th.Summary.FormatString = fomat;

            cel4Th.DataBindings.Add("Text", DataSource, "TongThuocVTYT");
            cel4Th.Summary.FormatString = fomat;

            cel5Th.DataBindings.Add("Text", DataSource, "ThuocVTMien");
            cel5Th.Summary.FormatString = fomat;

            cel6Th.DataBindings.Add("Text", DataSource, "ThuocVTKMien");
            cel6Th.Summary.FormatString = fomat;

            cel7Th.DataBindings.Add("Text", DataSource, "TongThuocVTYT");
            cel7Th.Summary.FormatString = fomat;

            cel8Th.DataBindings.Add("Text", DataSource, "soBNMien");
            cel8Th.Summary.FormatString = DungChung.Bien.FormatString[0];

            cel9Th.DataBindings.Add("Text", DataSource, "TongDonMien");
            cel9Th.Summary.FormatString = fomat;

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }
    }
}
