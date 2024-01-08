using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXN_CDM : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXN_CDM()
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
                        select new { bs.TenCB,bs.CapBac, }).ToList();
            if (bsdt.Count > 0)
            {
                colTenBSDT.Text =bsdt.First().CapBac+": "+  bsdt.First().TenCB;
            }
            var bsxn = (from bs in DataContect.CanBoes.Where(p=>p.MaCB==Macb.Value)
                        select new { bs.TenCB, bs.CapBac, }).ToList();
            if (bsxn.Count > 0)
            {
                colTenTKXN.Text = bsxn.First().CapBac + ": " + bsxn.First().TenCB;
            }
         
        }
        public void BindingData()
        {
            colYC.DataBindings.Add("Text", DataSource, "YC");
            colKQ.DataBindings.Add("Text", DataSource, "KQ");
            colTriSoBT.DataBindings.Add("Text", DataSource, "TSBT");
            col_Chon.DataBindings.Add("Text", DataSource, "Chon");
        }

    }
}
