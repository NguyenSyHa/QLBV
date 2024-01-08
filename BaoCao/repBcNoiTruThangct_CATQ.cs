using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repBcNoiTruThangct_CATQ : DevExpress.XtraReports.UI.XtraReport
    {
        public repBcNoiTruThangct_CATQ()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
   
        public void BindingData()
        {
            colDBBT.DataBindings.Add("Text", DataSource, "TenICD1");
            colMaBenh.DataBindings.Add("Text", DataSource, "MaICD1");
            //txtMax.DataBindings.Add("Text", DataSource, "Max");
            colTongSoLK.DataBindings.Add("Text", DataSource, "TongSoLK1").FormatString = DungChung.Bien.FormatString[1];
            colTongSoLKT.DataBindings.Add("Text", DataSource, "TongSoLK1").FormatString = DungChung.Bien.FormatString[1];
            colTreEm15.DataBindings.Add("Text", DataSource, "TongSoLKTE1").FormatString = DungChung.Bien.FormatString[1];
            colTE5.DataBindings.Add("Text", DataSource, "TongSoLKTE51").FormatString = DungChung.Bien.FormatString[1];
            colTreEmT.DataBindings.Add("Text", DataSource, "TongSoLKTE1").FormatString = DungChung.Bien.FormatString[1];
            colTE5T.DataBindings.Add("Text", DataSource, "TongSoLKTE51").FormatString = DungChung.Bien.FormatString[1];
            colNam.DataBindings.Add("Text", DataSource, "TongSoLKNam1").FormatString = DungChung.Bien.FormatString[1];
            colNamT.DataBindings.Add("Text", DataSource, "TongSoLKNam1").FormatString = DungChung.Bien.FormatString[1];
            colNu.DataBindings.Add("Text", DataSource, "TongSoLKNu1").FormatString = DungChung.Bien.FormatString[1];
            colNuT.DataBindings.Add("Text", DataSource, "TongSoLKNu1").FormatString = DungChung.Bien.FormatString[1];
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            TenCQ.Value = DungChung.Bien.TenCQ.ToUpper(); ;
      
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            colNguoiLapBang.Text = DungChung.Bien.NguoiLapBieu;
            colGiamDoc.Text = DungChung.Bien.GiamDoc;
        }
    
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
          
        }

        private void colDBBT_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (this.GetCurrentColumnValue("MaICD") != null || this.GetCurrentColumnValue("MaICD") != "")
            //{
            //    string _maicd = this.GetCurrentColumnValue("MaICD").ToString();
            //    var icd = (from maicd in _data.ICD10.Where(p => p.MaICD == _maicd) select new { maicd.TenICD }).ToList();
            //    if (icd.Count > 0)
            //    {
            //        colDBBT.Text = icd.First().TenICD;
            //    }
            //}
        }
    }

}