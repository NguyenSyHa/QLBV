using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuDoMatDoXuong_27001 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuDoMatDoXuong_27001()
        {
            InitializeComponent();
        }
        //QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            //string mabn = MaBNhan.Value.ToString();
            //var qhh = (from clsct in DataContect.CLScts
            //           join cls in DataContect.CLS on clsct.IdCLS equals cls.IdCLS
            //           where (cls.MaBNhan == mabn)
            //           select new { clsct.MaDVct, clsct.KetQua }).ToList();
            //if (qhh.Count > 0)
            //{
            //    try
            //    {
            //        txtKetQua.Text = qhh.Where(p => p.MaDVct== ("NoiSoi")).First().KetQua.ToString();
                   
            //    }
            //    catch { }
            //}
        }
        public void hienthiKQ(string str)
        {
            if (str == "")
                str = ".....................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................";
            xrKetQua.Html = str;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
