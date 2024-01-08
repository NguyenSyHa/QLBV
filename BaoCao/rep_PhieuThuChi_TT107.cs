using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuThuChi_TT107 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuThuChi_TT107()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtDonVi.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            txtQHNS.Text = "Mã QHNS:";
            xrTableCell71.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            xrTableCell73.Text = "Mã QHNS:";
            colDiaChi.Text = "Địa chỉ: " + DungChung.Ham.GetDiaChiBV();
            if (DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "27022")
            {
                clMaBNhan.Visible = true;
                clMaBNhan1.Visible = true;
            }
            else
            {
                clMaBNhan.Visible = false;
                clMaBNhan1.Visible = false;
            }

            ngayht.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            xrTableCell38.Text = DungChung.Bien.GiamDoc;
            xrTableCell39.Text = DungChung.Bien.KeToanTruong;
            xrTableCell142.Text = DungChung.Bien.GiamDoc;
            xrTableCell143.Text = DungChung.Bien.KeToanTruong;
            if(DungChung.Bien.MaBV == "27001")
            {
                xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
                xrTableCell90.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
                xrTableCell96.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
                xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
                xrTableCell26.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
                xrTableCell98.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            }
            if(DungChung.Bien.MaBV == "30010")
            {
                nguoithu1.Text = DungChung.Bien.NguoiLapBieu;
                nguoithu2.Text = DungChung.Bien.NguoiLapBieu;
            }
            //if(DungChung.Bien.MaBV == "27022")
            //{
            //    xrTable15.Visible = true;
            //    xrTable16.Visible = true;
            //}
            if (DungChung.Bien.MaBV == "27023")
                Detail.Visible = false;
            else
                ReportFooter.Visible = false;
        }
    }
}
