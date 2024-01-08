
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuKQSieuAm_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuKQSieuAm_A5()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            var bsxn = (from bs in DataContect.CanBoes.Where(p=>p.MaCB==Macb.Value)
                        select new { bs.TenCB }).ToList();
            if (bsxn.Count > 0)
            {
                colTenTKXN.Text = bsxn.First().TenCB;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (this.hienthiKL.Value != null) {
                bool a =Convert.ToBoolean(hienthiKL.Value);
                    xrLabel1.Visible = a;
                    xrLabel3.Visible = a;
            }
        }

      
    }
}
