
using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAm : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAm()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV =="24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "24272")
            {
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
                xrPictureBox3.Image = DungChung.Ham.GetLogo();
                colDiaChi2.Text = DungChung.Ham.GetDiaChiBV();
                colDiaChi3.Text = DungChung.Ham.GetDiaChiBV();
                if (DungChung.Bien.MaBV == "24272")
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = false;
                    SubBand3.Visible = true;
                    xrPictureBox2.Visible = false;
                    xrPictureBox3.Visible = false;
                    xrPictureBox5.Visible = true;
                    xrPictureBox5.Image = DungChung.Ham.GetLogo();
                }
            }
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
   
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                labmabn.Visible = true;
                //xrLabel5.Visible = true;
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "20001")
            {
                xrLine3.Visible = true;
                colTenCQCQ.Font = new Font("Times New Roman", 10f, FontStyle.Regular);
            }
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];

            if (DungChung.Bien.MaBV == "24009")
            {
                xrTable2.Visible = DungChung.Bien._Visible_CDHA[0];
                //xrTable5.Visible = DungChung.Bien._Visible_CDHA[1];
                SubBand6.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                tb_ChiDinh.Visible = true;
                tb_KetQua.Visible = true;
            }
            xrLabel63.Text = colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel68.Text = colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel104.Text = colTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel109.Text = colTenCQ1.Text = DungChung.Bien.TenCQ.ToUpper();

            if (MaCBDT.Value!=null)
            {
               colTenTKXN3.Text = colTenBSDT.Text = colTenTKXN4.Text = colTenTKXN2.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                
            }
            if (DungChung.Bien.MaBV == "02005" )
            {
                rowBSDieuTri.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
                xrTable5.Visible = DungChung.Bien._Visible_CDHA[1]; 
            }
            if (MaCBTH.Value!=null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, MaCBTH.Value.ToString());
                colTenTKXN1.Text = DungChung.Ham._getTenCB(DataContect, MaCBTH.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;

            if (DungChung.Bien.MaBV == "27194")
            {
                sdt27194.Visible = true;
                sdt27194_1.Visible = true;
                sdt27194.Text = "Số điên thoại: 0989770704";
                sdt27194_1.Text = "Số điên thoại: 0989770704";
            }
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "24272")
            {
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
                xrPictureBox3.Image = DungChung.Ham.GetLogo();
                colDiaChi2.Text = DungChung.Ham.GetDiaChiBV();
                colDiaChi3.Text = DungChung.Ham.GetDiaChiBV();
                if (DungChung.Bien.MaBV == "24272")
                {
                    SubBand1.Visible = false;
                    SubBand2.Visible = false;
                    SubBand3.Visible = true;
                    xrPictureBox2.Visible = false;
                    xrPictureBox3.Visible = false;
                    xrPictureBox5.Visible = true;
                    xrPictureBox5.Image = DungChung.Ham.GetLogo();
                }
            }
        }

        public void hienthiKQ(string str) 
        {
            colKetQua.Html = str;
            colKetQua1.Html = str;
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void xrPictureBox1_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrPictureBox1.ImageUrl = DuongDan.Value.ToString();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27194")
            {
                SubBand5.Visible = true;
                SubBand6.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24009") {
                lab19.Visible = false;
                xrPictureBox1.Visible = false;
            }
            //if (DungChung.Bien.MaBV == "30012")
            //{

            //    lab21.Visible = false;
            //    lab22.Visible = false;
            //    colTenTKXN.Visible = false;
            //}
            if (DungChung.Bien.MaBV == "27183")
            {
                xrLabel2.Text = "Mô tả";
                xrLabel2.Visible = true;
            }
            else if(DungChung.Bien.MaBV == "30010" && SubBand2.Visible == true)
            {
                xrLabel2.Text = "1. Mô tả tổn thương";
            }
            else
                xrLabel2.Text = "Mô tả tổn thương";
        }

        private void xrPictureBox4_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrPictureBox4.ImageUrl = DuongDan2.Value.ToString();
        }

        private void xrPictureBox6_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrPictureBox6.ImageUrl = DuongDan.Value.ToString();
        }
    }
}
