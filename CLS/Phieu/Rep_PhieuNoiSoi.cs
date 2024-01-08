using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoi()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }

        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                txtmabn.Visible = true;
                //xrRichText2.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
                lab2.Visible = false;
            }
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            if (DungChung.Bien.MaBV == "24009")
            {
                xrTable2.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            xrLabel28.Text = colTenCQ.Text = colTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel27.Text  = colTenCQCQ.Text = colTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrPictureBox1.Image = xrPictureBox2.Image = DungChung.Ham.GetLogo();
            colDiaChi.Text = colDiaChi2.Text = DungChung.Ham.GetDiaChiBV();
            if (MaCBDT.Value != null)
            {
                xrLabel47.Text = colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                tb_ChiDinh.Visible = true;
                tb_KetQua.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                txtmabn.LocationF = lab2.LocationF;
                colSo.Visible = lab3.Visible = lab2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand3.Visible = true;
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

    }
}
