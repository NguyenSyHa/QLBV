using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BCThuDichVuTuVienPhi_30003 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BCThuDichVuTuVienPhi_30003()
        {
            InitializeComponent();
        }

        string fomat = "{0:#,#}";
        internal void BindingData()
        {
            celKhoa.DataBindings.Add("Text", DataSource, "TenKP");
            celXQuang.DataBindings.Add("Text", DataSource, "XQ").FormatString = fomat;
            celCiti.DataBindings.Add("Text", DataSource, "Citi").FormatString = fomat;
            celSieuAm.DataBindings.Add("Text", DataSource, "SAM").FormatString = fomat;
            celDoLoangXuong.DataBindings.Add("Text", DataSource, "LoangXuong").FormatString = fomat;
            celTongKhoa.DataBindings.Add("Text", DataSource, "Tong").FormatString = fomat;

            celXQ_thang.DataBindings.Add("Text", DataSource, "XQ").FormatString = fomat;
            celCiti_Thang.DataBindings.Add("Text", DataSource, "Citi").FormatString = fomat;
            cel_SA_Thang.DataBindings.Add("Text", DataSource, "SAM").FormatString = fomat;
            cel_LoangXuong_thang.DataBindings.Add("Text", DataSource, "LoangXuong").FormatString = fomat;
            celTongKhoa_Thang.DataBindings.Add("Text", DataSource, "Tong").FormatString = fomat;

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {

        }
    }
}
