using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiTMH_01049 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiTMH_01049()
        {
            InitializeComponent();
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
                        xpicTaiTrai.ImageUrl = arrDD[i];
                        break;
                    case 2:
                        xpicTaiPhai.ImageUrl = arrDD[i];
                        break;
                    case 4:
                        xpicVom.ImageUrl = arrDD[i];
                        break;
                    case 5:
                        xpicHong.ImageUrl = arrDD[i];
                        break;
	            }
            }
            txttai.Text = arrKQ[0] + "; " + arrKQ[1];
            txtmui.Text = arrKQ[2] + "; " + arrKQ[3];
            txtvom.Text = arrKQ[4];
            txthong.Text = arrKQ[5];
            txtthanhquan.Text = arrKQ[6];
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
