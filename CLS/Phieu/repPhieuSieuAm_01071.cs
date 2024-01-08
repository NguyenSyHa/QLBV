
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAm_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAm_01071()
        {
            InitializeComponent();
            
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                labmabn.Visible = true;
                xrLabel5.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;

            }
            if (DungChung.Bien.MaBV == "12345")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
            
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }

            if ((DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 4) || (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 2) || (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 3))
            {
                Detail.Visible = true;
                ReportFooter.Visible = true;
            }
            if (DungChung.Bien.CheckInFull == 3 && DungChung.Bien.MaBV == "24297" )
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                Detail.Visible = false;
                ReportFooter.Visible = false;
            }
            if (DungChung.Bien.CheckInFull == 2 && DungChung.Bien.MaBV == "24297")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                Detail.Visible = true;
                ReportFooter.Visible = true;
            }
            if (DungChung.Bien.CheckInFull == 1 && DungChung.Bien.MaBV == "24297")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                Detail.Visible = true;
                ReportFooter.Visible = true;
            }
            if (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 4)
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                Detail.Visible = true;
                ReportFooter.Visible = true;
            }
            if (DungChung.Bien.MaBV != "01071" && DungChung.Bien.MaBV != "01049")
            {
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            else
            {
                tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
                tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            }
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            ReportFooter.Visible = DungChung.Bien._Visible_CDHA[1];
            ReportFooter.Visible = DungChung.Bien._Visible_CDHA[2];
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            colTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ1.Text = DungChung.Bien.TenCQ.ToUpper();

            if (MaCBDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "02005")
            {
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (Macb.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                colTenTKXN1.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                colTenCQCQ.Font = new Font(colTenCQCQ.Font, FontStyle.Regular);
                colTenCQ.Font = new Font(colTenCQ.Font, FontStyle.Regular);
                lab1.Font = new Font(lab1.Font, FontStyle.Regular);
                lab2.Font = new Font(lab2.Font, FontStyle.Regular);
                colSo.Font = new Font(colSo.Font, FontStyle.Regular);
                lab3.Font = new Font(lab3.Font, FontStyle.Regular);

                xrLabel5.Font = new Font(xrLabel5.Font, FontStyle.Regular);
                sovv.Font = new Font(sovv.Font, FontStyle.Regular);
                labmabn.Font = new Font(labmabn.Font, FontStyle.Regular);
                colYCKT.Font = new Font(colYCKT.Font, FontStyle.Regular);
                lab17.Font = new Font(lab17.Font, FontStyle.Regular);
                colTenBSDT.Font = new Font(colTenBSDT.Font, FontStyle.Regular);

                tb_KetQua.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)  | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                xrLabel33.Font = new Font(xrLabel33.Font, FontStyle.Regular);
                xrLabel34.Font = new Font(xrLabel34.Font, FontStyle.Regular);
                xrLabel36.Font = new Font(xrLabel36.Font, FontStyle.Regular);
                xrLabel38.Font = new Font(xrLabel38.Font, FontStyle.Regular);
                xrLabel4.Font = new Font(xrLabel4.Font, FontStyle.Regular);
                xrLabel6.Font = new Font(xrLabel6.Font, FontStyle.Regular);
                colTenCQ1.Font = new Font(colTenCQ1.Font, FontStyle.Regular);
                xrLabel21.Font = new Font(xrLabel21.Font, FontStyle.Regular);
                lab18.Font = new Font(lab18.Font, FontStyle.Regular);
                lab18.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                xrLabel2.Font = new Font(xrLabel2.Font, FontStyle.Regular);
                colKetQua.Font = new Font(colKetQua.Font, FontStyle.Regular);
                xrLabel1.Font = new Font(xrLabel1.Font, FontStyle.Regular);
                xrLabel3.Font = new Font(xrLabel3.Font, FontStyle.Regular);
                lab20.Font = new Font(lab20.Font, FontStyle.Regular);
                lab22.Font = new Font(lab22.Font, FontStyle.Regular);
                colTenTKXN.Font = new Font(colTenTKXN.Font, FontStyle.Regular);

                xrLabel5.LocationF = lab2.LocationF;
                var lf = lab2.LocationF;
                lf.X = labmabn.LocationF.X;
                labmabn.LocationF = lf;
                colSo.Visible = lab3.Visible = lab2.Visible = sovv.Visible = false;
            }

        }

        public void hienthiKQ(string str)
        {
            colKetQua.Html = str;
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

            if (DungChung.Bien.CheckInFull == 3 && DungChung.Bien.MaBV == "24297")
            {
                SubBand5.Visible = false;
                SubBand6.Visible = false;
            }
            if ((DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 4) || (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 2) || (DungChung.Bien.MaBV == "24297" && DungChung.Bien.CheckInFull == 1))
            {
                SubBand5.Visible = false;
                SubBand6.Visible = true;
            }
        }

        //private void xrPictureBox1_BeforePrint(object sender, CancelEventArgs e)
        //{
        //    this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
        //}

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {         

            if (DungChung.Bien.MaBV == "24009")
            {
                //lab19.Visible = false;
                //xrPictureBox1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30012")
            {

                lab21.Visible = false;
                lab22.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27183")
            {
                xrLabel2.Text = "Mô tả";
                xrLabel2.Visible = true;
            }
            else
                xrLabel2.Text = "Mô tả tổn thương";
        }
    }
}
