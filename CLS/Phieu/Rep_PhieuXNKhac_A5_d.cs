using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNKhac_A5_d : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNKhac_A5_d()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            string _bs = MaCBDT.Value.ToString();

            var bsdt = (from bs in DataContect.CanBoes
                        where (bs.MaCB == _bs)
                        select new { bs.TenCB }).ToList();
            if (bsdt.Count > 0)
            {
                colTenBSDT.Text = bsdt.First().TenCB;
            }
            var bsxn = (from bs in DataContect.CanBoes.Where(p=>p.MaCB==Macb.Value)
                        select new { bs.TenCB }).ToList();
            if (bsxn.Count > 0)
            {
                colTenTKXN.Text = bsxn.First().TenCB;
            }
         
        }
        public void BindingData()
        {
            colYC.DataBindings.Add("Text", DataSource, "YC");
            colKQ.DataBindings.Add("Text", DataSource, "KQ");
        }

    }
}
