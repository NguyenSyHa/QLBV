using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_phieusoket15ngaydt_30005 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_phieusoket15ngaydt_30005()
        {
            InitializeComponent();
        }
      

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            lab_TenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            lab_TenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            lab_ngaythang.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            lab_ngaythang1.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            cel_truongkhoa.Text = DungChung.Bien.TruongKhoaLS;
        }
    }
}
