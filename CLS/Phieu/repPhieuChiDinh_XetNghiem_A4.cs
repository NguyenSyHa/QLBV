using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class repPhieuChiDinh_XetNghiem_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        public repPhieuChiDinh_XetNghiem_A4()
        {
            InitializeComponent();
        }
        bool HienThiGH = false;
        public repPhieuChiDinh_XetNghiem_A4(bool hienthi)
        {
            InitializeComponent();
            HienThiGH = hienthi;
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                lab3.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
                colSo.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
            }

        }
        public void BindingData()
        {
            colYCKT.DataBindings.Add("Text", DataSource, "TenDV");
            colTenNhomDV.DataBindings.Add("Text", DataSource, "TenTN");
            //colSTT.DataBindings.Add("Text", DataSource, "STT");
            GroupHeader1.GroupFields.Add(new GroupField("STT"));
         

        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            colTenCQCQ.Text = DungChung.Bien.TenCQ.ToUpper();
            // colTenCQ.Text = DungChung.Bien.TenCQ;
            GroupHeader1.Visible = HienThiGH;
            if (DungChung.Bien.MaBV == "30009")
            {
                colSo.Visible = false;
                LAB_stt.Visible = false;
                PAR_stt.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                xrLabel12.Visible = true;
                xrLabel13.Visible = true;
                xrLabel14.Visible = true;
                xrLabel15.Visible = true;
                labsovv.Visible = true;
                lab2.Visible = false;
                xrLabel16.Visible = true;
                MaBNhan.Visible = true;
                xrLabel18.Visible = true;
            }
            if (DungChung.Bien.MaBV == "27001" || DungChung.Bien.MaBV == "30010")
                kpth.Visible = true;

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                lab1.Font = new Font(lab1.Font, FontStyle.Regular);
                lab2.Font = new Font(lab2.Font, FontStyle.Regular);
                colSo.Font = new Font(colSo.Font, FontStyle.Regular);
                lab3.Font = new Font(lab3.Font, FontStyle.Regular);
                xrLabel3.Font = new Font(xrLabel3.Font, FontStyle.Regular);
                xrLabel5.Font = new Font(xrLabel5.Font, FontStyle.Regular);
                xrLabel16.Font = new Font(xrLabel16.Font, FontStyle.Regular);
                labsovv.Font = new Font(labsovv.Font, FontStyle.Regular);
                xrLabel4.Font = new Font(xrLabel4.Font, FontStyle.Regular);
                xrTableCell3.Font = new Font(xrTableCell3.Font, FontStyle.Regular);
                xrTableCell4.Font = new Font(xrTableCell4.Font, FontStyle.Regular);
                xrLabel17.Visible = true;
                xrLabel18.Visible = true;
                lab54.Font = new Font(lab54.Font, FontStyle.Regular);
                colTenBSDT.Font = new Font(colTenBSDT.Font, FontStyle.Regular);

                //xrLabel5.LocationF = labmabn.LocationF = lab2.LocationF;
                colSo.Visible = LAB_stt.Visible = PAR_stt.Visible = lab3.Visible = lab2.Visible = labsovv.Visible = xrLabel16.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24297")
            {
                xrLabel5.Visible = false;
                xrLabel6.Visible = xrLabel7.Visible = false;
            }
            else
            {
                xrPictureBox2.Visible = false;
            }


        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint_1(object sender, CancelEventArgs e)
        {
            QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            colTenCQ.Text = DungChung.Bien.TenCQ;
            colTenCQCQ.Text = DungChung.Bien.TenCQCQ;
            if (MaCB.Value != null)
            {
                colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCB.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
                colTenBSDT.Visible = false;
        }

    }
}
