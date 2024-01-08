
using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.IO;

namespace QLBV.BaoCao
{
    public partial class repPhieuSieuAm_Mau : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuSieuAm_Mau()
        {
            InitializeComponent();
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);




        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            tb_ChiDinh.Visible = DungChung.Bien._Visible_CDHA[0];
            //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            //tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[2];
            if (DungChung.Bien.MaBV == "24009")
            {
               // tb_KetQua.Visible = DungChung.Bien._Visible_CDHA[1];
            }
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            colTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            txtSoYT.Text = "Điện thoại: " + DungChung.Bien.SDTCQ;
            // start HIS-1484
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
            {
                txtdichchi.Font = txtSoYT.Font = new Font("Times New Roman", 13, FontStyle.Bold);
                txtdichchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            }
            else
            {
                txtdichchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
            }
            // end HIS-1484 
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "30009")
                colSo.Visible = false;
            if (MaCBDT.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }

            if (DungChung.Bien.MaBV == "30004")
            {
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }

            if (DungChung.Bien.MaBV == "27183")
            {
                xrPictureBox3.Visible = true;
                colTenCQCQ.Visible = false;
                colTenCQ.Visible = false;
                xrLabel4.Visible = false;
            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                txtLuuY.Text = "Khi đến khám lại, bệnh nhân đem theo kết quả này! \r\n";
                if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                {
                    txtLuuY.Visible = false;
                    lblChuY12345.Text = "Khi đến khám lại, bệnh nhân đem theo kết quả này! \r\n";
                    lblChuY12345.Text += "Khám bệnh tất cả các ngày trong tuần \r\n";
                    if (QLBV.CLS.InPhieu._gettendv != 1)
                        lblChuY12345.Text += "Giảm 20% cho bệnh nhân BHYT";
                    xrPictureBox5.Visible = true;
                    xrPictureBox5.Image = DungChung.Ham.GetLogo();
                    xrPictureBox10.Visible = false;
                    if (DungChung.Bien.MaBV == "24297")
                    {
                        xrPictureBox10.Visible = false;
                        xrPictureBox5.Visible = false;
                    }
                    else
                        xrPictureBox4.Visible = false;
                    lblChuY12345.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;

                }
                else
                {
                    xrPictureBox5.Visible = false;
                    xrPictureBox10.Visible = true;
                    xrPictureBox4.Visible = false;
                }
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }
            //if(DungChung.Bien.MaBV == "01071")
            //{
            //    lab17.Text = "BÁC SỸ SIÊU ÂM";
            //    colTenBSDT.Visible = false;
            //}
            if (DungChung.Bien.MaBV == "30303")
            {
                xrLabel2.Text = "Mô tả:";
                this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);

            }
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
            if(DungChung.Bien.MaBV=="27001")
            {
                lab17.Visible = false;
                lab16.Visible = false;
                colTenBSDT.Visible = false;
            }    
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                SubBand3.Visible = true;
                SubBand4.Visible = false;
            }
            if (Macb.Value != null)
            {
                txtcbcd.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (MacbTH.Value != null)
            {
                txtcbth.Text = DungChung.Ham._getTenCB(DataContect, MacbTH.Value.ToString());
            }


        }
        public void hienthiKetQua(string str)
        {
            if (!string.IsNullOrEmpty(str))
                //colKetQua.Html = str;
                colKetQua1.Html = str;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "27023")
            {
                colKetQua.Visible = false;
                colKetQua1.Visible = true;
                DetailReport.Visible = true;
                Detail.Visible = false;
            } else
            {
                colKetQua.Visible = true;
                colKetQua1.Visible = false;
                DetailReport.Visible = false;
                Detail.Visible = true;
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                lab19.Visible = false;
                xrPictureBox1.Visible = false;
                xrPictureBox6.Visible = false;
                xrPictureBox2.Visible = false;
                xrPictureBox7.Visible = false;
            }
            string[] duongDanA1 = DuongDan.Value.ToString().Split(';');
            string[] duongDanA2 = DuongDan2.Value.ToString().Split(';');
            string s = DuongDan2.Value.ToString();
            //string t = @"\s|";
            ////string[] element = System.Text.RegularExpressions.Regex.Split(s,t);
            //string[] element = s.Split('|');
            //string _duongdan = element[0].ToString();
            if (DungChung.Bien.MaBV == "30372")
            {
                colKetQua1.Visible = false;
                if (DuongDan.Value.ToString() != "")
                {
                    this.xrPictureBox1.Image = duongDanA1[0]==""?null:Image.FromFile(duongDanA1[0].ToString());
                    this.xrPictureBox8.Image = duongDanA1[1]==""?null:Image.FromFile(duongDanA1[1].ToString());
                    this.xrPictureBox14.Image = duongDanA1[2]==""?null:Image.FromFile(duongDanA1[2].ToString());
                    this.xrPictureBox12.Image = duongDanA1[3] == "" ? null : Image.FromFile(duongDanA1[3].ToString());
                }
                //else
                //{
                //    xrPictureBox1.Image = null;
                //    xrPictureBox6.Image = null;
                //}
                if (DuongDan2.Value.ToString() != "")
                {
                    this.xrPictureBox2.Image = duongDanA2[0] == "" ? null : Image.FromFile(duongDanA2[0].ToString());
                    this.xrPictureBox9.Image = duongDanA2[1] == "" ? null : Image.FromFile(duongDanA2[1].ToString());
                    this.xrPictureBox13.Image = duongDanA2[2] == "" ? null : Image.FromFile(duongDanA2[2].ToString());
                    this.xrPictureBox11.Image = duongDanA2[3] == "" ? null : Image.FromFile(duongDanA2[3].ToString());
                    this.xrPictureBox7.Image = duongDanA2[0] == "" ? null : Image.FromFile(duongDanA2[0].ToString());
                }
                //else
                //{
                //    xrPictureBox2.Image = null;
                //    xrPictureBox7.Image = null;
                //}
            }
            else
            {
                if (DuongDan.Value != null && !String.IsNullOrEmpty(DuongDan.Value.ToString()))
                {
                   
                    xrPictureBox1.Image = Image.FromFile(duongDanA1[0]);
                    xrPictureBox6.Image = Image.FromFile(duongDanA1[0]);
                }
                else
                {
                    xrPictureBox1.Image = null;
                    xrPictureBox6.Image = null;
                }
                if (DuongDan2.Value != null && !String.IsNullOrEmpty(DuongDan2.Value.ToString()))
                {
                    if (File.Exists(duongDanA2[0]))
                        this.xrPictureBox2.Image = Image.FromFile(duongDanA2[0].ToString());
                    this.xrPictureBox7.Image = Image.FromFile(duongDanA2[0].ToString());
                   

                }
                else
                {
                    xrPictureBox2.Image = null;
                    this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
                    xrPictureBox7.Image = null;
                    this.xrPictureBox7.Borders = DevExpress.XtraPrinting.BorderSide.None;
                }
            }



            if (MacbTH.Value != null)
            {
                colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, MacbTH.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "27183")
            {
                lab18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrLabel2.Text = "Mô tả: ";
                xrLabel3.Font = new System.Drawing.Font("Time New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }
    }
}
