using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
namespace QLBV.BaoCao
{
    public partial class repThuChi_2lien_A5_27023 : DevExpress.XtraReports.UI.XtraReport
    {
        public repThuChi_2lien_A5_27023()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ.Text = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper();
            txtTenCQ2.Text = "Đơn vị: " + DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "20001")
            {
                xrTable8.Visible = false;
                xrTable11.Visible = false;
            }
            else
            {
                xrTable8.Visible = true;
                xrTable11.Visible = true;
            }

            GD.Value = DungChung.Bien.GiamDoc;
            KTT.Value = DungChung.Bien.KeToanTruong;
            NguoiLap.Value = DungChung.Bien.NguoiLapBieu;

            if (DungChung.Bien.MaBV == "27023")
            {
                lblGD11.Text = "Người lập biểu";
                lblGD21.Text = "Người lập biểu";

                //lblNguoiLap11.Text = "Người nhận tiền";
               // lblNguoiLap21.Text = "Người nhận tiền";
                lblNguoiLap12.Text = "(Ký, ghi rõ họ tên)";               
                lblNguoiLap22.Text = "(Ký, ghi rõ họ tên)";

                GD.Value = DungChung.Bien.NguoiLapBieu;
                NguoiLap.Value = "";
                KTT.Value = "";

                lblKTT11.Text = "";
                lblKTT12.Text = "";
                lblKTT21.Text = "";
                lblKTT22.Text = "";

                xrTable1.Visible = false;
                xrTable6.Visible = false;
            }
            else
            {
                xrTable1.Visible = true;
                xrTable6.Visible = true;
            }

            
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            
            
        }

        private void xrTableCell26_BeforePrint(object sender, CancelEventArgs e)
        {

        }

    }
}
