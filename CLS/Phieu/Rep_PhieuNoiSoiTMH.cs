using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiTMH : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiTMH()
        {
            InitializeComponent();
            
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777")
            {
                txtDiaChi.Text = DungChung.Ham.GetDiaChiBV();
                SubBand1.Visible = false;
                SubBand9.Visible = false;
                SubBand2.Visible = true;
                SubBand10.Visible = false;
                
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand9.Visible = false;
                SubBand2.Visible = false;
                SubBand10.Visible = true;
            }
            else
            {
                SubBand1.Visible = true;
                SubBand9.Visible = false;
                SubBand2.Visible = false;
                SubBand10.Visible = false;
            }
        }

        public void hienthi(string ketqua, string duongdan)
        {
            string[] arrKQ = QLBV_Ham.LayChuoi('|', ketqua);
            string[] arrDD = QLBV_Ham.LayChuoi('|', duongdan);
            for (int i = 0; i < arrKQ.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        xrTaiPhai.Rtf = arrKQ[i];
                        xpicTaiPhai.ImageUrl = arrDD[i];
                        xrPictureBox5.ImageUrl= arrDD[i];
                        xrPictureBox3.ImageUrl = arrDD[i];
                        break;
                    case 1:
                        xrTaiTrai.Rtf = arrKQ[i];
                        xpicTaiTrai.ImageUrl = arrDD[i];
                        break;
                    case 2:
                        xrMuiPhai.Rtf = arrKQ[i];
                        xpicMuiphai.ImageUrl = arrDD[i];
                        xrPictureBox6.ImageUrl = arrDD[i];
                        xrPictureBox4.ImageUrl = arrDD[i];
                        break;
                    case 3:
                        xrMuiTrai.Rtf = arrKQ[i];
                        xpicMuiTrai.ImageUrl = arrDD[i];
                        break;
                    case 4:
                        xrtVom.Rtf = arrKQ[i];
                        xpicVom.ImageUrl = arrDD[i];
                        xrPictureBox2.ImageUrl = arrDD[i];
                        xrPictureBox8.ImageUrl = arrDD[i];
                        break;
                    case 5:
                        xrHong.Rtf = arrKQ[i];
                        xpicHong.ImageUrl = arrDD[i];
                        xrPictureBox7.ImageUrl = arrDD[i];
                        xrPictureBox9.ImageUrl = arrDD[i];
                        break;
                    case 6:
                        xrThanhQuan.Rtf = arrKQ[i];
                        xpicTQ.ImageUrl = arrDD[i];
                      
                        break;
                }
            }
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = xrPictureBox10.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30009")
                colSoPhieu.Visible = false;
            xrLabel31.Text = TXTCQCQ.Text = TXTCQCQ2.Text = DungChung.Bien.TenCQCQ;
            xrLabel29.Text = txtTenBV.Text = txtTenBV2.Text = DungChung.Bien.TenCQ;
            xrLabel28.Text = txtDiaChi.Text = txtDiaChi2.Text = DungChung.Bien.DiaChi;
            xrLabel79.Text = TXTCQCQ.Text = TXTCQCQ2.Text = DungChung.Bien.TenCQCQ;
            xrLabel82.Text = txtTenBV.Text = txtTenBV2.Text = DungChung.Bien.TenCQ;
            xrLabel81.Text = txtDiaChi.Text = txtDiaChi2.Text = DungChung.Bien.DiaChi;
            if (DungChung.Bien.MaBV == "26007")
                xrBarCode1.Visible = true;
                
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30009") {
            //    colNgayCD.Visible = false;
            //    colBSDT.Visible = false;
            //    colBSK.Visible = false;
            //}

            if (DungChung.Bien.MaBV == "30012")
            {
                lblLoiDan.Visible = true;
                lblParaLoiDan.Visible = true;
            }
            else
            {
                lblLoiDan.Visible = false;
                lblParaLoiDan.Visible = false;
            }
        }


        private void xrTableCell17_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12001")
            {
                xrTableCell17.Text = "Hạ họng";
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
           
        }

        private void SubBand4_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
