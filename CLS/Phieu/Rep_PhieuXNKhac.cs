using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace QLBV.BaoCao
{
    public partial class Rep_PhieuXNKhac : DevExpress.XtraReports.UI.XtraReport
    {
        public Rep_PhieuXNKhac()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "27777" || DungChung.Bien.MaBV == "30372")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = true;
                xrPictureBox2.Image = DungChung.Ham.GetLogo();
                colDiaChi.Visible = true;
                colDiaChi.Text = DungChung.Ham.GetDiaChiBV();
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                SubBand1.Visible = false; SubBand2.Visible = false; SubBand3_01071_form_Covid.Visible = false;
                SubBand6.Visible = false;
                SubBand7.Visible = true;
                xrPictureBox4.Visible = true;
                xrPictureBox4.Image = DungChung.Ham.GetLogo();
            }
            else
            {
                SubBand1.Visible = true;
                SubBand2.Visible = false;
                
            }
        }
        QLBV_Database.QLBVEntities DataContect = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {


            if (DungChung.Bien.MaBV != "12345" && DungChung.Bien.MaBV != "24297")
            {
                xrLabel54.Visible = false;
                xrLabel55.Visible = false;
            }
            if (DungChung.Bien.MaBV == "26007")
            {
                xrBarCode1.Visible = true;
                xrTableCell4.Text = "Người thực hiện";
            }
            if (DungChung.Bien.MaBV == "27023")
            {
                this.Detail.HeightF = 47.04164F;
                this.xrTable4.SizeF = new System.Drawing.SizeF(738.7498F, 44.79167F);
                this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 44.79167F);
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                
                xrTable5.Visible = true;
                sovv.Visible = true;
                lab2.Visible = false;

                if (DungChung.Bien.MaBV == "01049")
                    xrLabel6.Visible = true;
            }

            if (DungChung.Bien.MaBV == "02005")
            {
                xrLabel4.Visible = false;
                colTenTKXN.Visible = false;
            }

            txtCQCQ.Text = xrLabel24.Text = colTenCQCQ.Text = colTenCQCQ24272.Text = colTenCQCQ12.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtCQ.Text = xrLabel28.Text = colTenCQ24272.Text = colTenCQ.Text = colTenCQ12.Text = DungChung.Bien.TenCQ.ToUpper();
            if (DungChung.Bien.MaBV == "27183")
            {
                xrPictureBox3.Visible = true;
                colTenCQCQ.Visible = false;
                colTenCQ.Visible = false;
                xrLabel5.Visible = false;
            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                xrLabel5.Visible = false;
                if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
                    lblKhoaXN.Visible = true;

            }
            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049" || DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297" || DungChung.Bien.MaBV == "56789")
            {
                thuong.Visible = true;
                thuong1.Visible = true;
                capcuu.Visible = true;
                capcuu1.Visible = true;
            }
            if (DungChung.Bien.MaBV == "27001")
            {
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                SubBand6.Visible = true;
                SubBand3_01071_form_Covid.Visible = false;
                xrTableCell3.Text = "Y BÁC SỸ";
                xrTableCell4.Text = "PHÒNG XÉT NGHIỆM";
                xrTableCell3.Visible = false;
                xrTableCell17.Visible = false;
                colTenBSDT.Visible = false;

            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "24297")
                xrTableCell4.Text = "PHÒNG XÉT NGHIỆM";

            if (DungChung.Bien.MaBV == "01071" || DungChung.Bien.MaBV == "01049")
            {
                xrTable5.LocationF = lab2.LocationF;
                xrLabel4.Visible = lab3.Visible = lab2.Visible = sovv.Visible = false; txtBarcode.Visible = lblBarcode.Visible = true;
                xrLabel5.Font = new System.Drawing.Font(xrLabel5.Font, FontStyle.Regular);
                lab1.Font = new System.Drawing.Font(lab1.Font, FontStyle.Regular);
                lab4.Font = new System.Drawing.Font(lab4.Font, FontStyle.Regular);
                xrLabel3.Font = new System.Drawing.Font(xrLabel3.Font, FontStyle.Regular);
                lab2.Font = new System.Drawing.Font(lab2.Font, FontStyle.Regular);
                lab3.Font = new System.Drawing.Font(lab3.Font, FontStyle.Regular);
                xrLabel4.Font = new System.Drawing.Font(xrLabel4.Font, FontStyle.Regular);
                xrTableCell2.Font = new System.Drawing.Font(xrTableCell2.Font, FontStyle.Regular);
                colMaBN.Font = new System.Drawing.Font(colMaBN.Font, FontStyle.Regular);
                xrLabel53.Font = new System.Drawing.Font(xrLabel53.Font, FontStyle.Regular);
                lab8.Font = new System.Drawing.Font(lab8.Font, FontStyle.Regular);
                xrLabel2.Font = new System.Drawing.Font(xrLabel2.Font, FontStyle.Regular);
                xrTableCell5.Font = new System.Drawing.Font(xrTableCell5.Font, FontStyle.Regular);
                xrTableCell9.Font = new System.Drawing.Font(xrTableCell9.Font, FontStyle.Regular);
                xrTableCell3.Font = new System.Drawing.Font(xrTableCell3.Font, FontStyle.Regular);
                xrTableCell4.Font = new System.Drawing.Font(xrTableCell4.Font, FontStyle.Regular);
                xrTableCell11.Font = new System.Drawing.Font(xrTableCell11.Font, FontStyle.Regular);
                xrTableCell10.Font = new System.Drawing.Font(xrTableCell10.Font, FontStyle.Regular);
                xrLabel26.Font = new System.Drawing.Font(xrLabel26.Font, FontStyle.Regular);
                xrLabel13.Font = new System.Drawing.Font(xrLabel13.Font, FontStyle.Regular);
                xrLabel40.Font = new System.Drawing.Font(xrLabel40.Font, FontStyle.Regular);
                xrTableCell1.Font = new System.Drawing.Font(xrTableCell1.Font, FontStyle.Regular);
                xrLabel16.Font = new System.Drawing.Font(xrLabel16.Font, FontStyle.Regular);
                xrLabel12.Font = new System.Drawing.Font(xrLabel12.Font, FontStyle.Regular);
                xrLabel29.Font = new System.Drawing.Font(xrLabel29.Font, FontStyle.Regular);
                lblDiaChi.Font = new System.Drawing.Font(lblDiaChi.Font, FontStyle.Regular);
                xrLabel28.Font = new System.Drawing.Font(xrLabel28.Font, FontStyle.Regular);
                xrLabel24.Font = new System.Drawing.Font(xrLabel24.Font, FontStyle.Regular);
                colTenCQ.Font = new System.Drawing.Font(colTenCQ.Font, FontStyle.Regular);
                colTenCQ12.Font = new System.Drawing.Font(colTenCQ.Font, FontStyle.Regular);
                colTenCQCQ.Font = new System.Drawing.Font(colTenCQCQ.Font, FontStyle.Regular);
                colTenCQCQ12.Font = new System.Drawing.Font(colTenCQCQ.Font, FontStyle.Regular);
                float currentsize= colTenBSDT.Font.Size;
                float currentsize2 = colTenBSDT.Font.Size;
                float currentsize3 = colTenTKXN.Font.Size;
                //colTenBSDT.Font = new System.Drawing.Font(colTenBSDT.Font.Name, currentsize, colTenBSDT.Font.Unit);
                //lblKhoaXN.Font = new System.Drawing.Font(lblKhoaXN.Font.Name, currentsize2, lblKhoaXN.Font.Unit);
                //colTenTKXN.Font = new System.Drawing.Font(colTenTKXN.Font.Name, currentsize3, colTenTKXN.Font.Unit);
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "04012")
                xrTableCell4.Text = "Trưởng khoa cận lâm sàng".ToUpper();
            if (MaCBDT.Value != null)
            {
                if (DungChung.Bien.MaBV == "27023")
                    colTenBSDT.Text = "Họ tên: " + DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
                else
                    colTenBSDT.Text = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            }
            if (Macb.Value != null)
            {
                var tencb = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.TenCB).FirstOrDefault();
                var chucVu = DataContect.CanBoes.Where(p => p.MaCB == Macb.Value).Select(p => p.CapBac).FirstOrDefault();
                if (DungChung.Bien.MaBV == "27023")
                    colTenBSDT.Text = "Họ tên: " + DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
                else if (DungChung.Bien.MaBV == "26007" || DungChung.Bien.MaBV == "30012")
                {

                    if (tencb != null && tencb != "")
                    {
                        colTenTKXN.Text = chucVu + ": " + tencb;
                    }
                }
                else if (DungChung.Bien.MaBV == "27001")
                    colTenTKXN.Text = tencb;
                else
                    colTenTKXN.Text = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            }
            if (DungChung.Bien.MaBV == "30004")
            {
                xrTableCell7.Visible = false;
                xrTableCell3.Visible = false;
                colTenBSDT.Visible = false;
                colTenTKXN.Visible = false;
            }
            if (DungChung.Bien.MaBV == "30005")
                xrTableCell4.Text = "TRƯỞNG KHOA XÉT NGHIỆM";
            if (DungChung.Bien.MaBV == "20001")
                xrTableCell4.Text = "TL. TRƯỞNG KHOA XÉT NGHIỆM";
            //if (DungChung.Bien.MaBV == "30009") { 
            //xrTableCell7.Visible=false;
            //    xrTableCell3.Visible=false;
            //    colTenBSDT.Visible = false;
            //}
        }
        public void BindingData()
        {
            colYC.DataBindings.Add("Text", DataSource, "YC");
            colKQ.DataBindings.Add("Text", DataSource, "KQ");
            colYC1.DataBindings.Add("Text", DataSource, "YC");
            colKQ1.DataBindings.Add("Text", DataSource, "KQ");
            colDV1.DataBindings.Add("Text", DataSource, "DV");
        }

        private void xrTableCell4_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "30007")
            {
                xrTableCell4.Text = "Bác Sỹ Cận Lâm Sàng".ToUpper();
            }
        }

        private void xrSubreport1_BeforePrint(object sender, CancelEventArgs e)
        {
            rep_PhieuXN_Sub repsub = (rep_PhieuXN_Sub)xrSubreport1.ReportSource;
            repsub.NGAYKIXN.Value = NgayTH.Value.ToString();
            repsub.NGAYKYDT.Value = NgayCD.Value.ToString();
            repsub.BSDT.Value = DungChung.Ham._getTenCB(DataContect, MaCBDT.Value.ToString());
            repsub.TKXN.Value = DungChung.Ham._getTenCB(DataContect, Macb.Value.ToString());
            repsub.tb12128.ForeColor = System.Drawing.Color.Black;
        }

        private void Rep_PhieuXNKhac_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "12128")
            {
                GroupFooter1.Visible = true;
                ReportFooter.Visible = false;
            }
        }

    }
}
