using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuThuChi_TT107_A5 : DevExpress.XtraReports.UI.XtraReport
    {
        public rep_PhieuThuChi_TT107_A5()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24012")
            {
                xrLabel2.Visible = false;
            } 
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtDonVi.Text = "Đơn vị: " + DungChung.Bien.TenCQ;
            txtQHNS.Text = "Mã QHNS:";
            if (DungChung.Bien.MaBV == "01830" || DungChung.Bien.MaBV == "27022")
            {
                clMaBNhan.Visible = true;
            }
            else
                clMaBNhan.Visible = false;
            ngayht.Text = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            if(DungChung.Bien.MaBV == "27001")
            {
                xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
                xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
                xrTableCell26.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            }
            if (DungChung.Bien.MaBV == "30010")
                nguoithu1.Text = DungChung.Bien.NguoiLapBieu;
            if (DungChung.Bien.MaBV == "24012")
            {
                xrTable14.Visible = true;
                xrTable2.Visible = false;
                xrLabel2.Visible = false;
                xrLabel4.Visible = true;
                xrLabel5.Visible = true;
                xrLabel6.Visible = true;
                xrLabel7.Visible = true;
                xrLabel8.Visible = true;
                xrLabel9.Visible = true;
                xrLabel10.Visible = true;
                xrLabel11.Visible = true;
                xrLabel12.Visible = true;
            }
           if(DungChung.Bien.MaBV == "30002")
           {
               SubBand1.Visible = false;
               SubBand2.Visible = false;
               sub30002.Visible = true;
               ReportFooter.Visible = false;
           }
            else if (DungChung.Bien.MaBV == "27023")
            {
                Detail.Visible = false;

            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                Detail24272.Visible = true;
                xrTableCell2.Text = "Mẫu số 01 - TT";
                xrTableCell4.Text = "(Ban hành theo Thông tư số 133/2016/TT-BTC";
                xrTableCell6.Text = "ngày 26/08/2016 của Bộ Tài chính)";
                txtDonVi.Text = DungChung.Bien.TenCQ; ;
                txtQHNS.Text = DungChung.Bien.DiaChi;
                txtSo24272.Visible = true;
                this.xrTableCell26.Font = new DevExpress.Drawing.DXFont("Times New Roman", 11.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
                ReportFooter.Visible = false;
            }
            else
                ReportFooter.Visible = false;
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "27022")
            {
                xrTableCell35.Text = "NGƯỜI NHẬN TIỀN";
                xrTableCell61.Text = "THỦ QUỸ";
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                xrLabel4.Visible = true;
                xrLabel5.Visible = true;
                xrLabel6.Visible = true;
                xrLabel7.Visible = true;
                xrLabel8.Visible = true;
                xrLabel9.Visible = true;
                xrLabel10.Visible = true;
                xrLabel11.Visible = true;
                xrLabel12.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                Detail24272.Visible = true;
                xrTableCell176.Text = DungChung.Bien.GiamDoc;
            }
        }

        private void sub30002_BeforePrint(object sender, CancelEventArgs e)
        {
            celNgayThangNam.Text = "Ngày " + DateTime.Now.Day.ToString("D2") + " tháng " + DateTime.Now.Month.ToString("D2") + " năm " + DateTime.Now.Year.ToString();
            if (DungChung.Bien.MaBV == "24012")
            {
                xrLabel4.Visible = true;
                xrLabel5.Visible = true;
                xrLabel6.Visible = true;
                xrLabel7.Visible = true;
                xrLabel8.Visible = true;
                xrLabel9.Visible = true;
                xrLabel10.Visible = true;
                xrLabel11.Visible = true;
                xrLabel12.Visible = true;
            }
        }
    }
}
