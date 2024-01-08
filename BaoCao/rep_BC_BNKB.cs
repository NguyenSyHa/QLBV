using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_BC_BNKB : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_BC_BNKB()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xlbTenCongTy.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xlbPhongKham.Text = DungChung.Bien.TenCQ.ToUpper();

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThang.Text = "Hà nội, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
        public void BindingData(){
            //celBS.DataBindings.Add("Text", DataSource, "");
            celBenhNhan.DataBindings.Add("Text", DataSource, "TenBNhan");
            celBS.DataBindings.Add("Text", DataSource, "TenCB");
        }
    }
}
