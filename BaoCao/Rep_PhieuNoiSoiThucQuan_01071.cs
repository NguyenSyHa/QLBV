using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiThucQuan_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiThucQuan_01071()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
       
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                txtmabn.Visible = true;
                xrRichText2.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (MaCBDT.Value!=null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004") {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (Macb.Value!=null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
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
            xrKetQua.Html = str;
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
         
        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {

        }


        internal void hienthiKQ2(string str)
        {
            xrKQ2.Html = str;
        }
    }
}
