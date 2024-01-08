using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.IO;

namespace QLBV.BaoCao
{
    public partial class repDonThuoc_TT18 : DevExpress.XtraReports.UI.XtraReport
    {
        private string nhomduoc;
        bool DonTra = false;

        public repDonThuoc_TT18(bool donTra = false)
        {
            DonTra = donTra;
            InitializeComponent();

            this.PaperKind = System.Drawing.Printing.PaperKind.A5;
            this.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            this.Name = "DonThuoc";

            if (DungChung.Bien.MaBV == "24009" || DungChung.Bien.MaBV == "12345")
            {
                ptAnh.Image = DungChung.Ham.GetLogo();
                SubBand3.Visible = false;
                SubBand4.Visible = true;
                SubBand5.Visible = false;

            }
            else if (DungChung.Bien.MaBV == "24297")
            {
                SubBand3.Visible = false;
                SubBand4.Visible = true;
                SubBand5.Visible = false;

            }
            else if (DungChung.Bien.MaBV == "30372")
            {
                xrPictureBox1.Image = DungChung.Ham.GetLogo();
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand5.Visible = true;

                lbl_DCBV.Text = DungChung.Ham.GetDiaChiBV();
                lbl_SDT.Text = "SĐT: " + DungChung.Ham.GetSDTBV() + " - " + "Fax: " + DungChung.Ham.GetFaxBV();
            }
            else if (DungChung.Bien.MaBV == "14017")
            {
                SubBand5.Visible = false;
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand6.Visible = false;
                SubBand9.Visible = true;
                SubBand8.Visible = true;
                xrLabel85.Text = DungChung.Bien.TenCQ;
                xrLabel86.Text = DungChung.Ham.GetSDTBV();
                xrLabel71.Visible = false;
            }
            else 
            {
                xrPictureBox2.Image = xrPictureBox3.Image = DungChung.Ham.GetLogo();
                if (DungChung.Bien.MaBV != "24272")
                {
                    SubBand5.Visible = false;
                    SubBand3.Visible = true;
                    SubBand4.Visible = false;
                    SubBand8.Visible = false;
                    SubBand6.Visible = true;
                }
                else
                {
                    if (DungChung.Bien.MaBV == "24272")
                    {
                        //sub_24272.Visible = true;
                        SubBand3.Visible = false;
                        SubBand4.Visible = false;
                        SubBand8.Visible = false;
                        SubBand6.Visible = false;
                        SubBand5.Visible = false;
                        SubBand9.Visible = false;
                        SB_Header_TraThuocNgoaiTru.Visible = false;
                    }
                }
            }
            if (!SubBand1.Visible)
                SubBand1.HeightF = 0;

            if (!SubBand2.Visible)
                SubBand2.HeightF = 0;

            if (!SubBand3.Visible)
                SubBand3.HeightF = 0;

            if (!SubBand4.Visible)
                SubBand4.HeightF = 0;

            if (!SubBand5.Visible)
                SubBand5.HeightF = 0;

            if (!SubBand6.Visible)
                SubBand6.HeightF = 0;

            if (!SubBand7.Visible)
                SubBand7.HeightF = 0;

            if (!SubBand8.Visible)
                SubBand8.HeightF = 0;

            if (!SubBand9.Visible)
                SubBand9.HeightF = 0;

            if (!SubBand10.Visible)
                SubBand10.HeightF = 0;
        }


        //QLBV_Database.QLBVEntities _data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
        }
        public void BindData()
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                TenThuoc.DataBindings.Add("Text", DataSource, "TenDV");
                SL.DataBindings.Add("Text", DataSource, "SoLuong");
                CD.DataBindings.Add("Text", DataSource, "HuongDan");
                DV_.DataBindings.Add("Text", DataSource, "DonVi");
                celMa.DataBindings.Add("Text", DataSource, "MaTam");
            }
            else if (DungChung.Bien.MaBV == "24272")
            {
                TenThuoc1.DataBindings.Add("Text", DataSource, "TenDVMain");
                SL1.DataBindings.Add("Text", DataSource, "SoLuong");
                CD1.DataBindings.Add("Text", DataSource, "HuongDan");
                DV1_.DataBindings.Add("Text", DataSource, "DonVi");
                TenDV.DataBindings.Add("Text", DataSource, "TenDV");
                SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
                DonVi.DataBindings.Add("Text", DataSource, "DonVi");
                HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");
                TenDV2.DataBindings.Add("Text", DataSource, "TenDV");
                SoLuong2.DataBindings.Add("Text", DataSource, "SoLuong");
                DonVi2.DataBindings.Add("Text", DataSource, "DonVi");
                HuongDan2.DataBindings.Add("Text", DataSource, "HuongDan");
            }
            else
            {
                TenDV.DataBindings.Add("Text", DataSource, "TenDV");
                SoLuong.DataBindings.Add("Text", DataSource, "SoLuong");
                DonVi.DataBindings.Add("Text", DataSource, "DonVi");
                HuongDan.DataBindings.Add("Text", DataSource, "HuongDan");
            }
        }
        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel27.Text = txtsodt.Text = lblDTCQ24272.Text = DungChung.Bien.SDTCQ;
            QLBV_Database.QLBVEntities data = new QLBV_Database.QLBVEntities(DungChung.Bien.StrCon);
            var _macqcq = data.BenhViens.Where(p => p.MaBV == DungChung.Bien.MaBV).FirstOrDefault();
            xrLabel28.Text = lblCQ24272.Text = txtTenCQ.Text = DungChung.Bien.MaBV == "27183" ? ("Công ty cổ phần bệnh viện Hà Nội Bắc Ninh" + "\n" + DungChung.Bien.TenCQ.ToUpper()) : DungChung.Bien.TenCQ.ToUpper();
            lblDiaChiCQ24272.Text = DungChung.Bien.DiaChi;
            
            if (DungChung.Bien.MaBV == "30002")
            {
                txttenBN.Text = "Chữ ký của bệnh nhân";
            }
            if (DungChung.Bien.MaBV == "24009" || (_macqcq != null && _macqcq.MaChuQuan == "24009"))
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "30009")
            {
                txtMaICD.Visible = true;
                txtICD.Visible = true;
            }
            if (DungChung.Bien.MaBV == "12345" || DungChung.Bien.MaBV == "56789" || DungChung.Bien.MaBV == "24297")
                xrTableCell5.Text = "Bác sỹ khám bệnh";
            if (DungChung.Bien.MaBV == "24297")
                xrLabel28.Text = "PHÒNG KHÁM ĐA KHOA TÂM VIỆT";

            if (DungChung.Bien.MaBV == "24012")
            {
                lblTuoi.Text = "Năm sinh: ";
                this.lblTuoi.LocationFloat = new DevExpress.Utils.PointFloat(0.2946218F, 117.2083F);
                this.lblTuoi.SizeF = new System.Drawing.SizeF(87.52016F, 22.99999F);
                this.xrLabel10.SizeF = new SizeF(156.3533F, 23.00002F);
                this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(83.5549F, 117.2083F);
                this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(250.4673F, 117.2083F);
                this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(334.6563F, 117.2082F);
            }
            if (DonTra)
            {
                this.txtTieuDe.Text = "PHIẾU TRẢ THUỐC";
                this.SubBand3.Visible = false;
                this.SB_Header_TraThuocNgoaiTru.Visible = true;
            }
            xrPictureBox2.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24272")
            {
                //sub_24272.Visible = true;
                SubBand3.Visible = false;
                SubBand4.Visible = false;
                SubBand8.Visible = false;
                SubBand6.Visible = false;
                SubBand5.Visible = false;
                SubBand9.Visible = false;
                SB_Header_TraThuocNgoaiTru.Visible = false;
            }

        }

        private void xrTableCell12_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "30002" || DungChung.Bien.MaBV == "30007" || DungChung.Bien.MaBV == "26007")
            {
                xrThuKho.Visible = true;
                xrThuKhoky.Visible = true;
                xrThuKhoNguoi.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "30004")
                xrTenBS.Visible = false;
            else if (DungChung.Bien.MaBV == "30009")
            {
                txtbnky.Visible = false;
                txttenBN.Visible = false;

            }
            else if (DungChung.Bien.MaBV == "20001")
            {
                SubBand2.Visible = true;
                SubBand1.Visible = false;
                xrLabel5.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "24297")
            {
                SubBand7.Visible = true;
                SubBand1.Visible = false;
                SubBand2.Visible = false;
            }
            else if (DungChung.Bien.MaBV == "27022")
            {
                SubBand2.Visible = false;
                SubBand1.Visible = false;
                SubBand10.Visible = true;
                xrLabel5.Visible = true;
            }
            else
            {
                SubBand2.Visible = false;
                xrLabel5.Visible = true;
            }
            if (DungChung.Bien.MaBV.Substring(0, 2) == "30" && DungChung.Bien.MaBV != "30007")
                xrTableCell5.Text = "Bác sỹ khám bệnh";
            if (DonTra)
            {
                this.SubBand1.Visible = false;
                this.SubBand2.Visible = false;
                this.SubBand7.Visible = false;
                this.SubBand10.Visible = false;
                this.SB_Footer_TraThuocNgoaiTru.Visible = true;
                this.TruongKhoa.Value = DungChung.Bien.TruongKhoaLS;
            }
        }

        private void SoLuong_BeforePrint(object sender, CancelEventArgs e)
        {

            if (this.GetCurrentColumnValue("SoLuong") != null)
            {
                int ot;
                if (Int32.TryParse(this.GetCurrentColumnValue("SoLuong").ToString(), out ot))
                {
                    int sl = Convert.ToInt32(this.GetCurrentColumnValue("SoLuong").ToString());
                    if (sl < 10)
                        SoLuong.Text = sl.ToString("D2");
                    else
                        SoLuong.Text = sl.ToString();

                }
            }
        }
    }
}
