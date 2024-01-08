using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuThuChi_TT107_A5_30004 : DevExpress.XtraReports.UI.XtraReport
    {
        QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        public rep_PhieuThuChi_TT107_A5_30004()
        {
            InitializeComponent();
            
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtDonVi.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            txtQHNS.Text = "Mã QHNS:";
            txtDonVi1.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            txtQHNS1.Text = "Mã QHNS:";
            //if (DungChung.Bien.MaBV == "01830")
            //{
            //    clMaBNhan.Visible = true;
            //}
            //else
            //    clMaBNhan.Visible = false;
            //ngayht.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
        }
    }
}
