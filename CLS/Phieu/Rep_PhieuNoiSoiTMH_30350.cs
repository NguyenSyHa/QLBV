using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QLBV_Library;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuNoiSoiTMH_30350 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuNoiSoiTMH_30350()
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
                       xrTaiTrai.Text = arrKQ[i];
                        xpicTaiTrai.ImageUrl = arrDD[i];
                        break;
                    case 1:
                        xrTaiPhai.Text = arrKQ[i];
                        xpicTaiPhai.ImageUrl = arrDD[i];
                        break;
                    case 4:
                        xrtVom.Text = arrKQ[i];
                        xpicVom.ImageUrl = arrDD[i];
                        break;
                    case 5:
                        xrHong.Text = arrKQ[i];
                        xpicHong.ImageUrl = arrDD[i];
                        break;
	            }
            }
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30009")
                colSoPhieu.Visible = false;
            TXTCQCQ.Text = DungChung.Bien.TenCQCQ;
            txtTenBV.Text = DungChung.Bien.TenCQ;
            txtDiaChi.Text = DungChung.Bien.DiaChi;
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
    }
}
