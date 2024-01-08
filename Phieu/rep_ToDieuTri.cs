using System;
using QLBV_Database;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace QLBV.Phieu
{
    public partial class rep_ToDieuTri : DevExpress.XtraReports.UI.XtraReport
    {
        int noitru = 0;
        int a = 0;
        int checkVisible = 1;
        int pageindex = 1;
        public rep_ToDieuTri(int _noitru)
        {
            InitializeComponent();
            noitru = _noitru;
            if (DungChung.Bien.MaBV != "30007")
            {
                ReportHeader.Visible = true;
                if (DungChung.Bien.MaBV == "24297")
                {
                    ReportHeader.Visible = false;
                    SubBand1.Visible = true;
                }
                if (DungChung.Bien.MaBV == "24012")
                {
                    HeaderSB_24012.Visible = true;
                    lblPageNumb.Visible = true;
                    lblPageNumb.Text = "1";
                    xrPageInfo1.Visible = false;
                }
                else
                {
                    lblPageNumb.Visible = false;
                    xrPageInfo1.Visible = true;
                }
            }
            else
                SubBand1.Visible = true;
        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            xrPictureBox1.Image = DungChung.Ham.GetLogo();
            if (DungChung.Bien.MaBV == "24009")
            {
                SubBand5.Visible = true;
                SubBand6.Visible = false;
            }
            else
            {
                if(DungChung.Bien.MaBV == "27001")
                {
                    xrLabel61.Visible = false;
                    xrPageInfo2.Visible = false;
                    xrLabel98.Visible = true;
                }    
                SubBand5.Visible = false;
                SubBand6.Visible = true;
            }
            txtTenCQ1.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            if (DungChung.Bien.MaBV == "30003")
            {
                SubBand3.Visible = false;
                SubBand4.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "24012")
            {
                SubBand4.Visible = false;
                SubBand3.Visible = false;
                SubBand6.Visible = false;
                SubBand1.Visible = false;
                SubBand2.Visible = false;
                HeaderSB_24012.Visible = true;
                TitleSB_24012.Visible = true;
                SB_24012.Visible = true;
            }
            else if (DungChung.Bien.MaBV == "27183")
            {
                SubBand7.Visible = true;
                SubBand6.Visible = false;
                SubBand2.Visible = true;
                SB_24012.Visible = false;
                SubBand4.Visible = false;
                SubBand3.Visible = true;
            }
            else
            {
                SubBand4.Visible = false;
                SubBand3.Visible = true;
            }
        }
        public void BindingData()
        {
            // colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhap")
            colNgayGio.DataBindings.Add("Text", DataSource, "NgayNhap");
            colDienbien.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLenh.DataBindings.Add("Text", DataSource, "YLenh");
            colTenCB.DataBindings.Add("Text", DataSource, "TenCB");
            TenBC1.DataBindings.Add("Text", DataSource, "TenCB");
            TenCB2.DataBindings.Add("Text", DataSource, "TenCB");
            //colTenCB00.DataBindings.Add("Text", DataSource, "TenCB");

            colNgayGio1.DataBindings.Add("Text", DataSource, "NgayNhap");
            colDienbien1.DataBindings.Add("Text", DataSource, "DienBien1");
            colYLenh1.DataBindings.Add("Text", DataSource, "YLenh");
            colTenCB1.DataBindings.Add("Text", DataSource, "TenCB");
            //colTenCB01.DataBindings.Add("Text", DataSource, "TenCB");

            colThoiGian2.DataBindings.Add("Text", DataSource, "NgayNhap");
            colKetLuan24012.DataBindings.Add("Text", DataSource, "DienBien1");
            colYL24012.DataBindings.Add("Text", DataSource, "YLenh");
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            if (noitru == 0 && (DungChung.Bien.MaBV == "30005" || DungChung.Bien.MaBV == "30280"))
            {
                this.colTenCB00.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
                this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
                this.colTenCB.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            }
            else
                GroupFooter1.Visible = false;
            if(DungChung.Bien.MaBV=="27001")
            {
                xrLabel97.Visible = true;
                TenBC1.Visible = true;
            }    
            if(DungChung.Bien.MaBV == "24272")
            {
                TenCB2.Visible = true;
            }    
        }

        private void PageHeader_BeforePrint(object sender, CancelEventArgs e)
        {
            txtTenCQ1.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel19.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel4.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel79.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel78.Text = DungChung.Bien.TenCQCQ.ToUpper();

            if (DungChung.Bien.MaBV == "27183")
            {
                if (KhoaDT.Name == "%Khoa%")
                {
                    xrLabel5.DataBindings.Add("Text", DataSource, "ChanDoan");
                }
                else
                {
                    xrLabel5.Text = "";
                }
            }

            if (DungChung.Bien.MaBV == "30007" || (DungChung.Bien.MaBV == "24297"))
            {
                if (a % 2 == 0)
                {
                    SubBand1.Visible = true;
                }
                else
                    SubBand1.Visible = false;
            }
            if (DungChung.Bien.MaBV == "24009")
            {
                SubBand1.Visible = false;
                SubBand7.Visible = false;
            }
            a++;
        }

        private void PageHeader_OnPrint(object sender, EventArgs e)
        {

        }
        private void PageHeader_AfterPrint(object sender, EventArgs e)
        {
            txtTenCQ1.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ1.Text = DungChung.Bien.TenCQCQ.ToUpper();
            txtTenCQ2.Text = DungChung.Bien.TenCQ.ToUpper();
            txtTenCQCQ2.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel19.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel4.Text = DungChung.Bien.TenCQCQ.ToUpper();
            xrLabel50.Text = DungChung.Bien.TenCQ.ToUpper();
            xrLabel58.Text = DungChung.Bien.TenCQCQ.ToUpper();
            //if (DungChung.Bien.MaBV == "30007")
            //{
            //    if (a % 2 == 1)
            //    {
            //        SubBand1.Visible = true;
            //    }
            //    else
            //        SubBand1.Visible = false;
            //}
            //a++;
        }

        private void HeaderSB_24012_BeforePrint(object sender, CancelEventArgs e)
        {
            if (DungChung.Bien.MaBV == "24012")
            {
                if (checkVisible % 2 == 0)
                {
                    HeaderSB_24012.Visible = false;
                }
                else
                    HeaderSB_24012.Visible = true;
                if (HeaderSB_24012.Visible == true)
                {
                    lblPageNumb.Text = pageindex.ToString();
                    pageindex++;
                }
                checkVisible++;
            }
        }

        private void colNgayGio_BeforePrint(object sender, CancelEventArgs e)
        {
            if(DungChung.Bien.MaBV == "27001")
            {
                if(!string.IsNullOrEmpty(colNgayGio.Text))
                    colNgayGio.Text = colNgayGio.Text.Remove(10);
            }
        }

        private void xrLabel76_BeforePrint(object sender, CancelEventArgs e)
        {
            var chandoan = xrLabel76.Text.Split(';');
            xrLabel76.Text = "";
            foreach (var item in chandoan)
            {
                xrLabel76.Text += item + "\n";
            }
        }
    }
}
