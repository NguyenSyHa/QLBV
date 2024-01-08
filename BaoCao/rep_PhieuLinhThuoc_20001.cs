using System;using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace QLBV.BaoCao
{
    public partial class rep_PhieuLinhThuoc_20001 : DevExpress.XtraReports.UI.XtraReport
    {
        
                   
        public rep_PhieuLinhThuoc_20001()
        {
            InitializeComponent();
            if (DungChung.Bien.MaBV == "14017" || DungChung.Bien.MaBV == "24297")
                this.PaperKind = System.Drawing.Printing.PaperKind.A5;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            celTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
            celTenCQ.Text = DungChung.Bien.TenCQ.ToUpper() ;
            if(DungChung.Bien.MaBV == "20001")
            {
                xrTableCell25.Text = "NGƯỜI PHÁT";
                xrTableCell27.Text = "NGƯỜI LĨNH";
                xrTableCell26.Text = "TRƯỞNG KHOA LÂM SÀNG";
                
            }

            if (DungChung.Bien.MaBV == "24297")
            {
                SubBand3.Visible = false;
                SubBand5.Visible = false;
                SubBand6.Visible = false;
                SubBand1.Visible = false;
                xrLabel26.Visible = false;
                SubBand2_24297.Visible = true;
                SubBand4_24297.Visible = true;
                SubBand5_24297.Visible = true;
            } 

            if (DungChung.Bien.MaBV == "27022")
            {
                SubBand1.Visible = false;
                PageFooter.Visible = true;
            }
            else
            {
                if (DungChung.Bien.MaBV == "14017")
                {
                    txtTenCQCQ.Text = DungChung.Bien.TenCQCQ.ToUpper();
                    txtTenCQ.Text = DungChung.Bien.TenCQ.ToUpper();
                    SubBand2_14017.Visible = true;
                    SubBand3.Visible = false;
                    SubBand5.Visible = false;
                    SubBand7_14017.Visible = true;
                    SubBand6.Visible = false;
                    SubBand1.Visible = false;
                    SubBand8_14017.Visible = true;
                    //txtSoThang14017.Visible = true;
                    //xrLabel41.Visible = false;
                    txtSoBA.Visible = true;
                    celTongTien.Visible = true;
                    xrLabel34.Visible = true;
                    xrLabel43.Visible = true;
                    xrLabel26.Visible = false;
                    tongso.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
                    xrTable8.Visible = xrTable9.Visible = xrTable10.Visible = false;
                    xrLabel23.Text = "Số: "; 

                }
                else
                {
                    xrTable14.Visible = xrTable15.Visible = xrTable16.Visible = false;
                }
                //SubBand1.Visible = true;
                PageFooter.Visible = false;
            }
        }

        public void BindingData()
        {
            //NgayKham.DataBindings.Add();
            maNB.DataBindings.Add("Text", DataSource, "MaTam");
            Tendv.DataBindings.Add("Text", DataSource, "TenDV");
            tendichvu.DataBindings.Add("Text", DataSource, "TenDV");
            Donvi.DataBindings.Add("Text", DataSource, "DonVi");
            xrTableCell82.DataBindings.Add("Text", DataSource, "DonVi");
            xrTableCell83.DataBindings.Add("Text", DataSource, "SoLuong");
            xrTableCell84.DataBindings.Add("Text", DataSource, "SoLuong");
            xrTableCell85.DataBindings.Add("Text", DataSource, "GhiChu");
            SLYC.DataBindings.Add("Text", DataSource, "SoLuong");
            SLPhat.DataBindings.Add("Text", DataSource, "SoLuong"); 
            MaQD.DataBindings.Add("Text", DataSource, "MaQD");
            celTenDV.DataBindings.Add("Text", DataSource, "TenDV");
            celDonVi.DataBindings.Add("Text", DataSource, "DonVi");
            celYeuCau.DataBindings.Add("Text", DataSource, "SoLuong");
            celPhat.DataBindings.Add("Text", DataSource, "SoLuong");
            celTongTien.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];
            celTongTien.Summary.FormatString = DungChung.Bien.FormatString[1];
            celTongTien1.DataBindings.Add("Text", DataSource, "Thanhtien").FormatString = DungChung.Bien.FormatString[1];
            //celTongTien1.Summary.FormatString = DungChung.Bien.FormatString[0];
            //24297
            xrTableCell91.DataBindings.Add("Text", DataSource, "TenDV");
            xrTableCell92.DataBindings.Add("Text", DataSource, "DonVi");
            xrTableCell93.DataBindings.Add("Text", DataSource, "DonGia");
            xrTableCell95.DataBindings.Add("Text", DataSource, "SoLuong");
            xrTableCell98.DataBindings.Add("Text", DataSource, "SoLuong");
            celTongTien2.DataBindings.Add("Text", DataSource, "Thanhtien");
            //END 24297


        }

        private void SubBand2_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV != "14017")
            {
                xrTable13.Visible = false;
            }
        }

        private void ReportFooter_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
            {
                tongso.Borders = DevExpress.XtraPrinting.BorderSide.All;
                xrTableCell58.Text = "Trưởng khoa lâm sàng";
            }
                
            
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "14017")
                xrTable11.Visible = false;
            else
                xrTable17.Visible = false;
        }
    }
}
