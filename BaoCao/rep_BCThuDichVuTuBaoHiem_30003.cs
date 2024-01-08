using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuDichVuTuBaoHiem_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuDichVuTuBaoHiem_30003()
        {
            InitializeComponent();
        }

        string fomat = DungChung.Bien.FormatString[1];
        internal void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celXQuang.DataBindings.Add("Text", DataSource, "XQ").FormatString = fomat;
            celCiti.DataBindings.Add("Text", DataSource, "Citi").FormatString = fomat;
            celSieuAm.DataBindings.Add("Text", DataSource, "SAM").FormatString = fomat;
            celDoLoangXuong.DataBindings.Add("Text", DataSource, "LoangXuong").FormatString = fomat;
            celTongKhoa.DataBindings.Add("Text", DataSource, "Tong").FormatString = fomat;

            celXQ_thang.DataBindings.Add("Text", DataSource, "XQ");
            celXQ_thang.Summary.FormatString = fomat;

            celCiti_Thang.DataBindings.Add("Text", DataSource, "Citi");
            celCiti_Thang.Summary.FormatString = fomat;

            cel_SA_Thang.DataBindings.Add("Text", DataSource, "SAM");
            cel_SA_Thang.Summary.FormatString = fomat;

            cel_LoangXuong_thang.DataBindings.Add("Text", DataSource, "LoangXuong");
            cel_LoangXuong_thang.Summary.FormatString = fomat;

            celTongKhoa_Thang.DataBindings.Add("Text", DataSource, "Tong");
            celTongKhoa_Thang.Summary.FormatString = fomat;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            //lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
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
