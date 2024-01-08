using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuCongKhaiDVKT_01071 : DevExpress.XtraReports.UI.XtraReport
    {
        //string[] Arr = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
        int n = 1;
        public rep_PhieuCongKhaiDVKT_01071()
        {
            InitializeComponent();
        }
        public void BindingData()
        {
            
                grten.DataBindings.Add("Text", DataSource, "STT");
                TenDV.DataBindings.Add("Text", DataSource, "TenDV");
                DonVi.DataBindings.Add("Text", DataSource, "DonVi");
                grstt.DataBindings.Add("Text", DataSource, "STTLM");
                grten1.DataBindings.Add("Text", DataSource, "STT");
                TenDV1.DataBindings.Add("Text", DataSource, "TenDV");
                DonVi1.DataBindings.Add("Text", DataSource, "DonVi");
                grstt1.DataBindings.Add("Text", DataSource, "STTLM");
                Ngay01.DataBindings.Add("Text", DataSource, "Sl1").FormatString = "{0:##,##.##}";
                Ngay02.DataBindings.Add("Text", DataSource, "Sl2").FormatString = "{0:##,##.##}";
                Ngay03.DataBindings.Add("Text", DataSource, "Sl3").FormatString = "{0:##,##.##}";
                Ngay04.DataBindings.Add("Text", DataSource, "Sl4").FormatString = "{0:##,##.##}";
                Ngay05.DataBindings.Add("Text", DataSource, "Sl5").FormatString = "{0:##,##.##}";
                Ngay06.DataBindings.Add("Text", DataSource, "Sl6").FormatString = "{0:##,##.##}";
                Ngay07.DataBindings.Add("Text", DataSource, "Sl7").FormatString = "{0:##,##.##}";
                Ngay08.DataBindings.Add("Text", DataSource, "Sl8").FormatString = "{0:##,##.##}";
                Ngay09.DataBindings.Add("Text", DataSource, "Sl9").FormatString = "{0:##,##.##}";
                Ngay010.DataBindings.Add("Text", DataSource, "Sl10").FormatString = "{0:##,##.##}";
                Ngay011.DataBindings.Add("Text", DataSource, "Sl11").FormatString = "{0:##,##.##}";
                Ngay012.DataBindings.Add("Text", DataSource, "Sl12").FormatString = "{0:##,##.##}";
                Ngay013.DataBindings.Add("Text", DataSource, "Sl13").FormatString = "{0:##,##.##}";
                Ngay014.DataBindings.Add("Text", DataSource, "Sl14").FormatString = "{0:##,##.##}";
                Ngay015.DataBindings.Add("Text", DataSource, "Sl15").FormatString = "{0:##,##.##}";
                Ngay016.DataBindings.Add("Text", DataSource, "Sl16").FormatString = "{0:##,##.##}";
                Ngay017.DataBindings.Add("Text", DataSource, "Sl17").FormatString = "{0:##,##.##}";
                Ngay018.DataBindings.Add("Text", DataSource, "Sl18").FormatString = "{0:##,##.##}";
                Ngay019.DataBindings.Add("Text", DataSource, "Sl19").FormatString = "{0:##,##.##}";
                Ngay020.DataBindings.Add("Text", DataSource, "Sl20").FormatString = "{0:##,##.##}";

                Ngay001.DataBindings.Add("Text", DataSource, "Sl1").FormatString = "{0:##,##.##}";
                Ngay002.DataBindings.Add("Text", DataSource, "Sl2").FormatString = "{0:##,##.##}";
                Ngay003.DataBindings.Add("Text", DataSource, "Sl3").FormatString = "{0:##,##.##}";
                Ngay004.DataBindings.Add("Text", DataSource, "Sl4").FormatString = "{0:##,##.##}";
                Ngay005.DataBindings.Add("Text", DataSource, "Sl5").FormatString = "{0:##,##.##}";
                Ngay006.DataBindings.Add("Text", DataSource, "Sl6").FormatString = "{0:##,##.##}";
                Ngay007.DataBindings.Add("Text", DataSource, "Sl7").FormatString = "{0:##,##.##}";
                Ngay008.DataBindings.Add("Text", DataSource, "Sl8").FormatString = "{0:##,##.##}";
                Ngay009.DataBindings.Add("Text", DataSource, "Sl9").FormatString = "{0:##,##.##}";
                Ngay0010.DataBindings.Add("Text", DataSource, "Sl10").FormatString = "{0:##,##.##}";
                Ngay0011.DataBindings.Add("Text", DataSource, "Sl11").FormatString = "{0:##,##.##}";
                Ngay0012.DataBindings.Add("Text", DataSource, "Sl12").FormatString = "{0:##,##.##}";
                Ngay0013.DataBindings.Add("Text", DataSource, "Sl13").FormatString = "{0:##,##.##}";
                Ngay0014.DataBindings.Add("Text", DataSource, "Sl14").FormatString = "{0:##,##.##}";
                Ngay0015.DataBindings.Add("Text", DataSource, "Sl15").FormatString = "{0:##,##.##}";
                Ngay0016.DataBindings.Add("Text", DataSource, "Sl16").FormatString = "{0:##,##.##}";
                Ngay0017.DataBindings.Add("Text", DataSource, "Sl17").FormatString = "{0:##,##.##}";
                Ngay0018.DataBindings.Add("Text", DataSource, "Sl18").FormatString = "{0:##,##.##}";
                Ngay0019.DataBindings.Add("Text", DataSource, "Sl19").FormatString = "{0:##,##.##}";
                Ngay0020.DataBindings.Add("Text", DataSource, "Sl20").FormatString = "{0:##,##.##}";
                Ngay0021.DataBindings.Add("Text", DataSource, "Sl21").FormatString = "{0:##,##.##}";
                Ngay0022.DataBindings.Add("Text", DataSource, "Sl22").FormatString = "{0:##,##.##}";
                Ngay0023.DataBindings.Add("Text", DataSource, "Sl23").FormatString = "{0:##,##.##}";
                Ngay0024.DataBindings.Add("Text", DataSource, "Sl24").FormatString = "{0:##,##.##}";
                Ngay0025.DataBindings.Add("Text", DataSource, "Sl25").FormatString = "{0:##,##.##}";
                //Ngay0026.DataBindings.Add("Text", DataSource, "Sl26").FormatString = "{0:##,##.##}";
                //Ngay0027.DataBindings.Add("Text", DataSource, "Sl27").FormatString = "{0:##,##.##}";
                //Ngay0028.DataBindings.Add("Text", DataSource, "Sl28").FormatString = "{0:##,##.##}";
                //Ngay0029.DataBindings.Add("Text", DataSource, "Sl29").FormatString = "{0:##,##.##}";
                //Ngay0030.DataBindings.Add("Text", DataSource, "Sl30").FormatString = "{0:##,##.##}";
            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    Ngay01_Tong.DataBindings.Add("Text", DataSource, "Sl1").FormatString = "{0:##,##.##}";
            //    Ngay02_Tong.DataBindings.Add("Text", DataSource, "Sl2").FormatString = "{0:##,##.##}";
            //    Ngay03_Tong.DataBindings.Add("Text", DataSource, "Sl3").FormatString = "{0:##,##.##}";
            //    Ngay04_Tong.DataBindings.Add("Text", DataSource, "Sl4").FormatString = "{0:##,##.##}";
            //    Ngay05_Tong.DataBindings.Add("Text", DataSource, "Sl5").FormatString = "{0:##,##.##}";
            //    Ngay06_Tong.DataBindings.Add("Text", DataSource, "Sl6").FormatString = "{0:##,##.##}";
            //    Ngay07_Tong.DataBindings.Add("Text", DataSource, "Sl7").FormatString = "{0:##,##.##}";
            //    Ngay08_Tong.DataBindings.Add("Text", DataSource, "Sl8").FormatString = "{0:##,##.##}";
            //    Ngay09_Tong.DataBindings.Add("Text", DataSource, "Sl9").FormatString = "{0:##,##.##}";
            //    Ngay10_Tong.DataBindings.Add("Text", DataSource, "Sl10").FormatString = "{0:##,##.##}";
            //    Ngay11_Tong.DataBindings.Add("Text", DataSource, "Sl11").FormatString = "{0:##,##.##}";
            //    Ngay12_Tong.DataBindings.Add("Text", DataSource, "Sl12").FormatString = "{0:##,##.##}";
            //    Ngay13_Tong.DataBindings.Add("Text", DataSource, "Sl13").FormatString = "{0:##,##.##}";
            //    Ngay14_Tong.DataBindings.Add("Text", DataSource, "Sl14").FormatString = "{0:##,##.##}";
            //    Ngay15_Tong.DataBindings.Add("Text", DataSource, "Sl15").FormatString = "{0:##,##.##}";
            //    Ngay16_Tong.DataBindings.Add("Text", DataSource, "Sl16").FormatString = "{0:##,##.##}";
            //    Ngay17_Tong.DataBindings.Add("Text", DataSource, "Sl17").FormatString = "{0:##,##.##}";
            //    Ngay18_Tong.DataBindings.Add("Text", DataSource, "Sl18").FormatString = "{0:##,##.##}";
            //    Ngay19_Tong.DataBindings.Add("Text", DataSource, "Sl19").FormatString = "{0:##,##.##}";
            //    Ngay20_Tong.DataBindings.Add("Text", DataSource, "Sl20").FormatString = "{0:##,##.##}";
            //}
            GroupHeader1.GroupFields.Add(new GroupField("St"));
            
        }

        private void grstt_BeforePrint(object sender, CancelEventArgs e)
        {
            //for (int i = 0; i < Arr.Length; i++)
            //{
                //grstt.Text = Arr[i];
                
            //}
            if (DungChung.Bien.MaBV == "30007")
               this.grten.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         }

        private void GroupFooter1_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void GroupHeader1_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                if (this.GetCurrentColumnValue("STT") != null)
                {
                    string stt = this.GetCurrentColumnValue("STT").ToString();
                    if (stt != "Khám bệnh")
                    {
                        GroupFooter1.Visible = true; GroupFooter2.Visible = false;
                    }
                    else
                    {
                        GroupFooter1.Visible = false;
                        GroupFooter2.Visible = true;
                    }
                }
                
            }
            else
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable6.Visible = false;
                tb24012_herder.Visible = true;
            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell1.Text = xrTableCell427.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrTableCell3.Text = xrTableCell429.Text = DungChung.Bien.TenCQ.ToUpper();
            xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "30007")
                xrTableCell225.Text = "Tổng";
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
            }
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable4.Visible = false;
                tb24012_detail.Visible = true;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                repro24012.Visible = true;
                xrTable5.Visible = false;
            }
        }


        private void PageHeader_BeforePrint_1(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable3.Visible = false;
                tb24012.Visible = true;
            }
        }

        private void xrTable5_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
