using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class 
        BangChamCom : DevExpress.XtraReports.UI.XtraReport
    {
        public BangChamCom()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtcq.Text = txtcq2.Text = DungChung.Bien.TenCQ.ToUpper();
            SubBand1.Visible = true;
            if (DungChung.Bien.MaBV == "24272")
            {
                SubBand2.Visible = false;
                SubBand3.Visible = true;
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
            }

        }

        public void BindingData()
        {
                
                SubBand1.Visible = true;
                
                SubBand4.Visible = true;
                txtten.DataBindings.Add("Text", DataSource, "TenBNhan");
                txtdiachi.DataBindings.Add("Text", DataSource, "DChi");
                x1.DataBindings.Add("Text", DataSource, "X1");
                x2.DataBindings.Add("Text", DataSource, "X2");
                x3.DataBindings.Add("Text", DataSource, "X3");
                x4.DataBindings.Add("Text", DataSource, "X4");
                x5.DataBindings.Add("Text", DataSource, "X5");
                x6.DataBindings.Add("Text", DataSource, "X6");
                x7.DataBindings.Add("Text", DataSource, "X7");
                x8.DataBindings.Add("Text", DataSource, "X8");
                x9.DataBindings.Add("Text", DataSource, "X9");
                x10.DataBindings.Add("Text", DataSource, "X10");
                x11.DataBindings.Add("Text", DataSource, "X11");
                x12.DataBindings.Add("Text", DataSource, "X12");
                x13.DataBindings.Add("Text", DataSource, "X13");
                x14.DataBindings.Add("Text", DataSource, "X14");
                x15.DataBindings.Add("Text", DataSource, "X15");
                x16.DataBindings.Add("Text", DataSource, "X16");
                x17.DataBindings.Add("Text", DataSource, "X17");
                x18.DataBindings.Add("Text", DataSource, "X18");
                x19.DataBindings.Add("Text", DataSource, "X19");
                x20.DataBindings.Add("Text", DataSource, "X20");
                x21.DataBindings.Add("Text", DataSource, "X21");
                x22.DataBindings.Add("Text", DataSource, "X22");
                x23.DataBindings.Add("Text", DataSource, "X23");
                x24.DataBindings.Add("Text", DataSource, "X24");
                x25.DataBindings.Add("Text", DataSource, "X25");
                x26.DataBindings.Add("Text", DataSource, "X26");
                x27.DataBindings.Add("Text", DataSource, "X27");
                x28.DataBindings.Add("Text", DataSource, "X28");
                x29.DataBindings.Add("Text", DataSource, "X29");
                x30.DataBindings.Add("Text", DataSource, "X30");
                x31.DataBindings.Add("Text", DataSource, "X31");
                SoNgay.DataBindings.Add("Text", DataSource, "SoXuat");

                x1_Tong.DataBindings.Add("Text", DataSource, "X1");
                x2_Tong.DataBindings.Add("Text", DataSource, "X2");
                x3_Tong.DataBindings.Add("Text", DataSource, "X3");
                x4_Tong.DataBindings.Add("Text", DataSource, "X4");
                x5_Tong.DataBindings.Add("Text", DataSource, "X5");
                x6_Tong.DataBindings.Add("Text", DataSource, "X6");
                x7_Tong.DataBindings.Add("Text", DataSource, "X7");
                x8_Tong.DataBindings.Add("Text", DataSource, "X8");
                x9_Tong.DataBindings.Add("Text", DataSource, "X9");
                x10_Tong.DataBindings.Add("Text", DataSource, "X10");
                x11_Tong.DataBindings.Add("Text", DataSource, "X11");
                x12_Tong.DataBindings.Add("Text", DataSource, "X12");
                x13_Tong.DataBindings.Add("Text", DataSource, "X13");
                x14_Tong.DataBindings.Add("Text", DataSource, "X14");
                x15_Tong.DataBindings.Add("Text", DataSource, "X15");
                x16_Tong.DataBindings.Add("Text", DataSource, "X16");
                x17_Tong.DataBindings.Add("Text", DataSource, "X17");
                x18_Tong.DataBindings.Add("Text", DataSource, "X18");
                x19_Tong.DataBindings.Add("Text", DataSource, "X19");
                x20_Tong.DataBindings.Add("Text", DataSource, "X20");
                x21_Tong.DataBindings.Add("Text", DataSource, "X21");
                x22_Tong.DataBindings.Add("Text", DataSource, "X22");
                x23_Tong.DataBindings.Add("Text", DataSource, "X23");
                x24_Tong.DataBindings.Add("Text", DataSource, "X24");
                x25_Tong.DataBindings.Add("Text", DataSource, "X25");
                x26_Tong.DataBindings.Add("Text", DataSource, "X26");
                x27_Tong.DataBindings.Add("Text", DataSource, "X27");
                x28_Tong.DataBindings.Add("Text", DataSource, "X28");
                x29_Tong.DataBindings.Add("Text", DataSource, "X29");
                x30_Tong.DataBindings.Add("Text", DataSource, "X30");
                x31_Tong.DataBindings.Add("Text", DataSource, "X31");
                SoNgay_Tong.DataBindings.Add("Text", DataSource, "SoXuat");
            
            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTableCell607.Text = DungChung.Bien.GiamDoc;
        }
    }
}
