using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
//using System.Text;
//using System.Data;

namespace QLBV.BaoCao
{
    public partial class rep_phieusieuamdoppler_tim : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_phieusieuamdoppler_tim()
        {
            InitializeComponent();
        }
        public string[] ketqua = new string[15];
        public string[] ketquac = new string[6];

        public string ketluans;
        public string loirans;
        public string hotes;
        public string dichis;
        public string tuois;
        public string khoas;
        public string yeucaus;
        public string chuandoans;
        public string CBTT;
        public string BSCD;
        public string ngaythuchiens;
        public string ngaychidinhs;
        public string bhyts;
        

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                xrLabel1.ForeColor = System.Drawing.Color.Red;
                CQCQ.ForeColor = System.Drawing.Color.SteelBlue;
                CQUAN.ForeColor = System.Drawing.Color.SteelBlue;

                txtdichchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
                txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
                txtdichchi.Font = txtSoYT.Font = new Font("Times New Roman", 13, FontStyle.Bold);
                txtdichchi.Text = "Địa chỉ: " + DungChung.Bien.DiaChi;
                txtSoYT.Text = "Điện thoại: " + DungChung.Bien.SDTCQ;

                xrTable6.Visible = true;
                xrTable1.Visible = false;

                SubBand1.Visible = false;
                SubBand5.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        public void hamloatphieu()
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            CQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            CQUAN.Text = DungChung.Bien.TenCQ.ToUpper();
            HOten.Text = colHT.Text = hotes;
            Diachi.Text = colDC.Text = dichis;
            khoa.Text = colKhoa.Text = khoas;
            yeucausieuam.Text = yeucaus;
            tuoi.Text = colTuoi.Text = tuois;
            Chuandoan.Text = colCD.Text = chuandoans;

            HOten1.Text = colHT.Text = hotes;
            Diachi1.Text = colDC.Text = dichis;
            khoa1.Text = colKhoa.Text = khoas;
            yeucausieuam1.Text = yeucaus;
            tuoi1.Text = colTuoi.Text = tuois;
            Chuandoan1.Text = colCD.Text = chuandoans;

            so1.Text = ketqua[0];
            so2.Text = ketqua[1];
            so3.Text = ketqua[2];
            so4.Text = ketqua[3];
            so5.Text = ketqua[4];
            so6.Text = ketqua[5];
            so7.Text = ketqua[6];
            so8.Text = ketqua[7];
            so9.Text = ketqua[8];
            so10.Text = ketqua[9];
            so11.Text = ketqua[10];
            so12.Text = ketqua[11];
            so13.Text = ketqua[12];
            so14.Text = ketqua[13];
            so15.Text = ketqua[14];
            s1.Text = ketqua[0];
            s2.Text = ketqua[1];
            s3.Text = ketqua[2];
            s4.Text = ketqua[3];
            s5.Text = ketqua[4];
            s6.Text = ketqua[5];
            s7.Text = ketqua[6];
            s8.Text = ketqua[7];
            s9.Text = ketqua[8];
            s10.Text = ketqua[9];
            s11.Text = ketqua[10];
            s12.Text = ketqua[11];
            s13.Text = ketqua[12];
            if (DungChung.Bien.MaBV == "24297")
            {
                string[] arrListStr0 = ketquac[0].Split('|');
                if (arrListStr0[0] != null)
                {
                    van.Text = arrListStr0[0];
                }
                if (arrListStr0[1] != null)
                {
                    van2.Text = arrListStr0[1];
                }
                string[] arrListStr1 = ketquac[1].Split('|');
                if (arrListStr1[0] != null)
                {
                    dongmach.Text = arrListStr1[0];
                }
                if (arrListStr1[1] != null)
                {
                    dongmach2.Text = arrListStr1[1];
                }
                string[] arrListStr2 = ketquac[2].Split('|');
                if (arrListStr2[0] != null)
                {
                    phoi.Text = arrListStr2[0];
                }
                if (arrListStr2[1] != null)
                {
                    phoi2.Text = arrListStr2[1];
                }
                string[] arrListStr3 = ketquac[3].Split('|');
                if (arrListStr3[0] != null)
                {
                    vanbala.Text = arrListStr3[0];
                }
                if (arrListStr3[1] != null)
                {
                    vanbala2.Text = arrListStr3[1];
                }
                string[] arrListStr4 = ketquac[4].Split('|');
                if (arrListStr4.Length == 2)
                {
                    if (arrListStr4[0] != null)
                    {
                        mangtim.Text = arrListStr4[0];
                        if (arrListStr4[1] != null)
                        {
                            mangtim2.Text = arrListStr4[1];
                        }
                    }
                }
                else
                {
                    mangtim.Text = ketquac[4];
                }

            }
            else
            {
                van.Text = ketquac[0];
                dongmach.Text = ketquac[1];
                phoi.Text = ketquac[2];
                vanbala.Text = ketquac[3];
                mangtim.Text = ketquac[4];
            }
            ketluan.Text = ketluans;
            loidan.Text = loirans;
            TENcbtt.Text = CBTT;

            ketluan1.Text = loirans;
            loidan1.Text = ketluans;
            TENcbtt1.Text = CBTT;

            bcchidinh.Text = colBSCD.Text = BSCD;
            bcchidinh1.Text = colBSCD.Text = BSCD;
            ngaychidinh.Text = ngaychidinhs;
            if (!string.IsNullOrEmpty(ngaythuchiens))
            {
                ngaythuchien.Text = ngaythuchiens;
                ngaythuchien1.Text = ngaythuchiens;
            }
            colBHYT.Text = bhyts;

            if (DungChung.Bien.MaBV == "30372")
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
                xrLabel25.Text = "6. Nhận xét khác:";
                xrLabel2.Text = "7. Kết luận:";
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
            }

        }

        private void ReportFooter_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                SubBand4.Visible = true;
                SubBand3.Visible = false;
                xrLabel20.ForeColor = System.Drawing.Color.Blue;
                xrLabel21.ForeColor = System.Drawing.Color.Blue;
                xrLabel22.ForeColor = System.Drawing.Color.Blue;
                xrLabel23.ForeColor = System.Drawing.Color.Blue;
                xrLabel24.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24297")
            {
                xrTable7.Visible = true;
                xrTable2.Visible = false;
            }
        }
    }
}
