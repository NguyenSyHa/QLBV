using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class repGiayRaVien : DevExpress.XtraReports.UI.XtraReport
    {
        public repGiayRaVien()
        {
            InitializeComponent();

        }

        private void repGiayRaVien_BeforePrint(object sender, CancelEventArgs e)
        {
            //xrLabel1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                xrLabel74.Text = "Năm sinh/Tuổi:";
                xrLabel89.Visible = false;
                xrLine4.Visible = true;
            }
            else
            {
                xrLabel28.Visible = true;
                xrLine4.Visible = false;
            }
            if (DungChung.Bien.MaBV == "27023")
            {
                //xrLabel2.Text = "BỆNH VIỆN PHỔI";
                //xrLabel28.Visible = false;
                //xrLine2.Visible = true;
                //xrLine1.Visible = false;
                //xrLabel33.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //xrLabel11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel20.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel21.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel22.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel24.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel16.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel17.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
                //xrLabel19.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular);
            }
            else
            {
                xrLabel103.Text = xrLabel65.Text = DungChung.Bien.TenCQ.ToUpper();
                xrLabel102.Text = xrLabel64.Text = DungChung.Bien.TenCQCQ.ToUpper();
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                ptanh.Image = DungChung.Ham.GetLogo();
            }
            if (DungChung.Bien.MaBV == "14017")
            {
                xrLabel23.Visible = false;
                xrLabel27.Visible = false;
                xrLabel32.Visible = false;

            }
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

            if (SThe.Value != null && SThe.Value.ToString() != "")
            {

                col1.Text = SThe.Value.ToString().Substring(0, 2);
                col2.Text = SThe.Value.ToString().Substring(2, 1);
                col3.Text = SThe.Value.ToString().Substring(3, 2);
                col4.Text = SThe.Value.ToString().Substring(5, 2);
                col5.Text = SThe.Value.ToString().Substring(7, 3);
                col6.Text = SThe.Value.ToString().Substring(10, 5);

                xrTableCell1.Text = SThe.Value.ToString().Substring(0, 2);
                xrTableCell2.Text = SThe.Value.ToString().Substring(2, 1);
                xrTableCell3.Text = SThe.Value.ToString().Substring(3, 2);
                xrTableCell4.Text = SThe.Value.ToString().Substring(5, 2);
                xrTableCell5.Text = SThe.Value.ToString().Substring(7, 3);
                xrTableCell6.Text = SThe.Value.ToString().Substring(10, 5);
            }
            if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "12001")//a hùng y/c 26-06 a quý y/c ngày 18-04-2019
            {
                xrLabel26.Text = "Giám đốc";
                if (DungChung.Bien.MaBV == "26007")
                    xrLabel26.Text = "Thủ trưởng";
                //Detail.Visible = true;
                //ReportFooter.Visible = false;
                //if (DungChung.Bien.MaBV == "12001")
                //    xrLabel47.Text = "Thủ trưởng đơn vị";
            }
            //if(DungChung.Bien.MaBV == "27023")
            //{
            //    xrTable1.Visible = false;
            //    xrLabel52.Visible = true;

            //}
            //else
            //{
            //    xrTable1.Visible = true;
            //    xrLabel52.Visible = false;
            //}

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "27023")
            {
                GiamDoc.Value = DungChung.Bien.GiamDoc;
            }
            if (DungChung.Bien.MaBV == "14018")
            {
                GiamDoc.Value = "";    
            }
               
            //if (DungChung.Bien.MaBV == "04018" || DungChung.Bien.MaBV == "26007")
            //    colTD_GDBV.Text = "GIÁM ĐỐC";
        }

        private void xrLabel26_BeforePrint(object sender, CancelEventArgs e)
        {
            //if (DungChung.Bien.MaBV == "08204" || DungChung.Bien.MaBV == "26062")
            //{
            //    xrLabel26.Text = "BÁC SĨ ĐIỀU TRỊ";
            //}
            //else 
            //{
            //    xrLabel26.Text = " TRƯỞNG KHOA ĐIỀU TRỊ";
            //}
            //if (DungChung.Bien.MaBV == "26062")
            //{
            //    colTD_GDBV.Text = "BỆNH XÁ TRƯỞNG";
            //}
            //else
            //{
            //    colTD_GDBV.Text = " GIÁM ĐỐC BỆNH VIỆN";
            //}
            //if (DungChung.Bien.MaBV == "27021")
            //    xrLabel19.Text = "Ghi chú:";
            //if (DungChung.Bien.MaBV == "04018" || DungChung.Bien.MaBV == "26007")
            //    colTD_GDBV.Text = "GIÁM ĐỐC";
        }

    }
}
