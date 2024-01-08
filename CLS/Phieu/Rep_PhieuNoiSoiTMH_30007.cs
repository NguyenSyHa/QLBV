using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiTMH_30007 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiTMH_30007()
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
                        xrTaiPhai.Text ="- " + arrKQ[i].ToUpper();
                        xpicTaiPhai.ImageUrl = arrDD[i];
                        break;
                    case 1:
                        xrTaiTrai.Text = "- " + arrKQ[i].ToUpper();
                        //xpicTaiTrai.ImageUrl = arrDD[i];
                        break;
                    case 2:
                        xrMuiPhai.Text = "- " + arrKQ[i].ToUpper();
                        xpicMuiphai.ImageUrl = arrDD[i];
                        break;
                    case 3:
                        xrMuiTrai.Text = "- " + arrKQ[i].ToUpper();
                        //xpicMuiTrai.ImageUrl = arrDD[i];
                        break;
                    case 4:
                        //xrtVom.Text = "- " + arrKQ[i].ToUpper();
                        //xpicVom.ImageUrl = arrDD[i];
                        break;
                    case 5:
                        xrHong.Text = "- " + arrKQ[i].ToUpper();
                        xpicHong.ImageUrl = arrDD[i];
                        break;
                    case 6:
                        xrThanhQuan.Text = "- " + arrKQ[i].ToUpper();
                        xpicTQ.ImageUrl = arrDD[i];
                        break;
	            }
            }
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30009")
            //    colSoPhieu.Visible = false;
            //TXTCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenBV.Text = DungChung.Bien.TenCQ.ToUpper();
            txtDiaChi.Text = "Đ/C: " + DungChung.Bien.DiaChi.ToUpper();
        }
        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "30009") {
            //    colNgayCD.Visible = false;
            //    colBSDT.Visible = false;
            //    colBSK.Visible = false;
            //}
        
        }


        private void xrTableCell17_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "12001")
            //{
            //    xrTableCell17.Text = "Hạ họng";
            //}
        }

        private void Rep_PhieuNoiSoiTMH_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {


        }
    }
}
